using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Commands.Create.CreateClientCommand;
using ClinicManager.Application.Commands.Create.CreateDoctorCommand;
using ClinicManager.Application.Commands.Create.CreateLoginCommand;
using ClinicManager.Application.Commands.Create.CreateMetricsCommand;
using ClinicManager.Application.Commands.Create.CreateMonitoringCommand;
using ClinicManager.Application.Commands.Create.CreatePatientCommand;
using ClinicManager.Application.Commands.Create.CreateServiceClinicCommand;
using ClinicManager.Application.Commands.Create.CreateServiceCommand;
using ClinicManager.Application.Commands.Create.CreateUserCommand;
using ClinicManager.Application.Commands.Delete.DeleteMonitoringCommand;
using ClinicManager.Application.Commands.Update.UpdateClientCommand;
using ClinicManager.Application.Commands.Update.UpdateMonitoringCommand;
using ClinicManager.Application.Commands.Update.UpdateMetricsCommand;
using ClinicManager.Application.Queries.GetAllClients;
using ClinicManager.Application.Queries.GetAllMonitoringsByIdPaginated;
using ClinicManager.Application.Queries.GetAllMonitoringsPaginated;
using ClinicManager.Application.Queries.GetAllUsersPaginated;
using ClinicManager.Application.Queries.GetMetricsByClientId;
using ClinicManager.Application.Queries.GetIdClient;
using ClinicManager.Application.Queries.GetIdDoctor;
using ClinicManager.Application.Queries.GetIdPatient;
using ClinicManager.Application.Queries.GetIdService;
using ClinicManager.Application.Queries.GetIdServiceClinic;
using ClinicManager.Application.Validators;
using ClinicManager.Infrastructure.Persistence;
using ClinicManager.Infrastructure.Persistence.Repositories;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using ClinicManagerAPI.DTO_s;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

string chaveSecreta = "6baf3137-314c-4af5-90cf-24b86066eb65";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "RTI CardioPerformance - API", Version = "v1" });

    var securitySchems = new OpenApiSecurityScheme
    {
        Name = "JWT Autenticação",
        Description = "Entre com o JWT Bearer Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    x.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchems);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchems, new string[] { } }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging() // Isso é útil para depuração
                   .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateMetricsCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateDoctorCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreatePatientCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateServiceClinicCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateServiceCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateClientCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(UpdateClientCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(UpdateMetricsCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateLoginCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateMonitoringCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(DeleteMonitoringCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(UpdateMonitoringCommand)));

builder.Services.AddValidatorsFromAssemblyContaining<AttachmentCreateDTOValidator>().AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<AttachmentCreateDTO>, AttachmentCreateDTOValidator>();

builder.Services.AddTransient<IRequestHandler<GetDoctorByIdQuery, Doctor>, GetDoctorByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetClientByIdQuery, ResponseBase<Client>>, GetClientByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllClientsQuery, ResponseBase<PaginatedList<Client>>>, GetAllClientsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetPatientByIdQuery, Patient>, GetPatientByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetServiceByIdQuery, Service>, GetServiceByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetServiceClinicByIdQuery, ServiceClinic>, GetServiceClinicByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllUsersPaginatedQuery, ResponseBase<PaginatedList<User>>>, GetAllUsersPaginatedQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllMonitoringsPaginatedQuery, ResponseBase<PaginatedList<Monitoring>>>, GetAllMonitoringsPaginatedQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllMonitoringsByIdPaginatedQuery, ResponseBase<PaginatedList<Monitoring>>>, GetAllMonitoringsByIdPaginatedQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetMetricsByClientIdQuery, ResponseBase<Metrics>>, GetMetricsByClientIdQueryHandler>();


builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IServiceClinicRepository, ServiceClinicRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IMonitoringRepository, MonitoringRepository>();
builder.Services.AddScoped<IMetricsRepository, MetricsRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddLogging();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

// Usar CORS
app.UseCors("AllowAllOrigins");

// Middleware para logar requests e responses
app.Use(async (context, next) =>
{
    // Logar request
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

    // Logar headers
    foreach (var header in context.Request.Headers)
    {
        Console.WriteLine($"Header: {header.Key} = {header.Value}");
    }

    await next.Invoke();

    // Logar response
    Console.WriteLine($"Response: {context.Response.StatusCode}");

    // Logar headers
    foreach (var header in context.Response.Headers)
    {
        Console.WriteLine($"Header: {header.Key} = {header.Value}");
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

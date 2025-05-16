using Microsoft.EntityFrameworkCore;
using Tutorial10.RestAPI;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UniversityDatabase") ?? 
                       throw new InvalidOperationException("University connection string not found"); 

builder.Services.AddDbContext<SampleComanyContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/jobs", async (SampleComanyContext context, CancellationToken token) => {
    try
    {
        var jobs = await context.Jobs.ToListAsync(token);
        return Results.Ok(jobs);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    
});

app.MapGet("/api/departments", () => {
    
});

app.MapGet("/api/employees", async (SampleComanyContext context, CancellationToken token) =>
{
    try
    {
        var employees = await context.Employees.ToListAsync(token);
        var employeesDTO = new List<EmployeeDTO>();
        foreach (var employee in employees)
        {
            employeesDTO.Add(new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                HireDate = employee.HireDate,
                JobId = employee.JobId,
                DepartmentId = employee.DepartmentId,
                Salary = employee.Salary,
                Commission = employee.Commission
            });
        }

        return Results.Ok(employeesDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/employees/{id}", async (int id, SampleComanyContext context, CancellationToken token) =>
{
    try
    {
        var employee = await context.Employees.FindAsync(token, id);

        if (employee != null)
        {
            return Results.Ok(new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                HireDate = employee.HireDate,
                JobId = employee.JobId,
                DepartmentId = employee.DepartmentId,
                Salary = employee.Salary,
                Commission = employee.Commission
            });
        }
        else
        {
            return Results.NotFound();
        }

    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/api/employees", () =>
{
    
});

app.MapPut("/api/employees/{id}", (int id) =>
{
    
});

app.MapDelete("/api/employees/{id}", (int id) =>
{
    
});

app.Run();

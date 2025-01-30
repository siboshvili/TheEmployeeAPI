using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TheEmployeeAPI;
using static Employee;

public class EmployeesController : BaseController
{
    private readonly IRepository<Employee> _repository;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IRepository<Employee> repository, ILogger<EmployeesController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        var employees = _repository.GetAll().Select(EmployeeToGetEmployeeResponse);

        return Ok(employees);
    }

    [HttpGet("{employeeId}/benefits")]
    public IActionResult GetEmployeeById(int employeeId)
    {
        var employee = _repository.GetById(employeeId);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee.Benefits.Select(BenefitToBenefitResponse));

    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest employeeRequest)
    {
        var validationResults = await ValidateAsync(employeeRequest);
        if (!validationResults.IsValid)
        {
            return ValidationProblem(validationResults.ToModelStateDictionary());
        }

        var newEmployee = new Employee
        {
            FirstName = employeeRequest.FirstName!,
            LastName = employeeRequest.LastName!,
            SocialSecurityNumber = employeeRequest.SocialSecurityNumber,
            Address1 = employeeRequest.Address1,
            Address2 = employeeRequest.Address2,
            City = employeeRequest.City,
            State = employeeRequest.State,
            ZipCode = employeeRequest.ZipCode,
            PhoneNumber = employeeRequest.PhoneNumber,
            Email = employeeRequest.Email,

        };

        _repository.Create(newEmployee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeRequest employeeRequest)
    {
        _logger.LogInformation("Updating employee with ID: {EmployeeId}", id);

        var existingEmployee = _repository.GetById(id);
        if (existingEmployee == null)
        {
            _logger.LogWarning("Employee with ID: {EmployeeId} not found", id);
            return NotFound();
        }

        _logger.LogDebug("Updating employee details for ID: {EmployeeId}", id);
        existingEmployee.Address1 = employeeRequest.Address1;
        existingEmployee.Address2 = employeeRequest.Address2;
        existingEmployee.City = employeeRequest.City;
        existingEmployee.State = employeeRequest.State;
        existingEmployee.ZipCode = employeeRequest.ZipCode;
        existingEmployee.PhoneNumber = employeeRequest.PhoneNumber;
        existingEmployee.Email = employeeRequest.Email;

        try
        {
            _repository.Update(existingEmployee);
            _logger.LogInformation("Employee with ID: {EmployeeId} successfully updated", id);
            return Ok(existingEmployee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating employee with ID: {EmployeeId}", id);
            return StatusCode(500, "An error occurred while updating the employee");
        }
    }

    private static GetEmployeeResponse EmployeeToGetEmployeeResponse(Employee employee)
    {
        return new GetEmployeeResponse
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Address1 = employee.Address1,
            Address2 = employee.Address2,
            City = employee.City,
            State = employee.State,
            ZipCode = employee.ZipCode,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
            Benefits = employee.Benefits.Select(BenefitToBenefitResponse).ToList()
        };
    }

    private static GetEmployeeResponseEmployeeBenefit BenefitToBenefitResponse(EmployeeBenefits benefit)
    {
        return new GetEmployeeResponseEmployeeBenefit
        {
            Id = benefit.Id,
            EmployeeId = benefit.EmployeeId,
            BenefitType = benefit.BenefitType,
            Cost = benefit.Cost
        };

    }
}


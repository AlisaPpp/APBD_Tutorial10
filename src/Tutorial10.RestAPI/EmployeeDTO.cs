namespace Tutorial10.RestAPI;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int JobId { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public decimal? Commission { get; set; }
    public int DepartmentId { get; set; }

}
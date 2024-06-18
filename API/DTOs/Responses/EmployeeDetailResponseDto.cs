namespace API.DTOs.Responses;

public class EmployeeDetailResponseDto
{
    public EmployeeDetailResponseDto()
    {
        
    }
    
    public EmployeeDetailResponseDto(Guid id, string nik, string fullName, string userName, string email, string phoneNumber, DateOnly hireDate, int salary, float comissionPct, string managerNik, string managerName, string jobTitle, string department, string city)
    {
        Id = id;
        Nik = nik;
        FullName = fullName;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        HireDate = hireDate;
        Salary = salary;
        ComissionPct = comissionPct;
        ManagerNik = managerNik;
        ManagerName = managerName;
        JobTitle = jobTitle;
        Department = department;
        City = city;
    }

    public Guid Id { get; init; }
    public string Nik { get; init; }
    public string FullName { get; init; } 
    public string UserName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateOnly HireDate { get; init; }
    public int Salary { get; init; } 
    public float ComissionPct { get; init; } 
    public string ManagerNik { get; init; } 
    public string ManagerName { get; init; } 
    public string JobTitle { get; init; } 
    public string Department { get; init; }
    public string City { get; init; } 
}

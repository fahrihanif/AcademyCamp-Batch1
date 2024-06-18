using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;

namespace API.Services.Interfaces;

public interface IEmployeeService : IGeneralService<EmployeeRequestDto, EmployeeResponseDto>
{
    Task<(IEnumerable<EmployeeDetailResponseDto> mapEmployeeDetail, int count)> GetEmployeeDetails(EmployeeDetailRequestDto request);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Dtos;
using Ultatel.Core.Entities;

namespace Ultatel.Services.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
        Task<IEnumerable<Student>> SearchAsync(StudentSearchDto searchDto, string userId);
    }
}


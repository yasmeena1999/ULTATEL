using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Entities;

namespace Ultatel.Services.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync(string userId);
        Task<Student> GetStudentByIdAsync(int id, string userId);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id, string userId);
    }
}

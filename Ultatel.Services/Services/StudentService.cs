using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Dtos;
using Ultatel.Core.Entities;
using Ultatel.Core.Interfaces;
using Ultatel.Data.Repositories;

namespace Ultatel.Services.Services
{
    public class StudentService: IStudentService {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
        }

        public async Task UpdateAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
        }

        public async Task DeleteAsync(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Student>> SearchAsync(StudentSearchDto searchDto, string userId)
        {
            return await _studentRepository.SearchAsync(searchDto, userId);
        }
    }
}
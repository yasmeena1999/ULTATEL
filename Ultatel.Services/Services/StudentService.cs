using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Dtos;
using Ultatel.Core.Entities;
using Ultatel.Core.Enums;
using Ultatel.Core.Interfaces;
using Ultatel.Data.Repositories;

namespace Ultatel.Services.Services
{
    public class StudentService : IStudentService
    {
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
            var query = _studentRepository.GetAllAsync().Result.AsQueryable();

            // Always filter by userId first
            query = query.Where(s => s.AddedByUserId == userId);

            if (!string.IsNullOrEmpty(searchDto.Name))
            {
                string[] nameParts = searchDto.Name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (nameParts.Length > 0)
                {
                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[1] : "";

                    query = query.Where(s => (s.FirstName + " " + s.LastName).Contains(searchDto.Name));
                }
            }
            if (!string.IsNullOrEmpty(searchDto.Country))
            {
                query = query.Where(s => s.Country.Contains(searchDto.Country));
            }
            if (!string.IsNullOrEmpty(searchDto.Gender) && Enum.TryParse<GenderValue>(searchDto.Gender, out var genderEnum))
            {
                query = query.Where(s => s.Gender == genderEnum);
            }
            if (searchDto.AgeFrom.HasValue)
            {
                query = query.Where(s => s.Age >= searchDto.AgeFrom);
            }
            if (searchDto.AgeTo.HasValue)
            {
                query = query.Where(s => s.Age <= searchDto.AgeTo);
            }

            var result = await query.ToListAsync();

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }
    }
}
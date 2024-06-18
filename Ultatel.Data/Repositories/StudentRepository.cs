using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Dtos;
using Ultatel.Core.Entities;
using Ultatel.Core.Enums;
using Ultatel.Core.Interfaces;
using Ultatel.Data.Data;

namespace Ultatel.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> SearchAsync(StudentSearchDto searchDto, string userId)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Name))
            
            {
                string[] nameParts = searchDto.Name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (nameParts.Length > 0)
                {
                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[1] : "";

                    query = query.Where(s => (s.FirstName + " " + s.LastName).Contains(searchDto.Name) && s.AddedByUserId == userId);
                }
            }
            if (!string.IsNullOrEmpty(searchDto.Country))
            {
                query = query.Where(s => s.Country.Contains(searchDto.Country) && s.AddedByUserId == userId);
            }
            if (!string.IsNullOrEmpty(searchDto.Gender) && Enum.TryParse<GenderValue>(searchDto.Gender, out var genderEnum))
            {
                query = query.Where(s => s.Gender == genderEnum && s.AddedByUserId == userId);
            }
            if (searchDto.AgeFrom.HasValue)
            {
                query = query.Where(s => s.Age >= searchDto.AgeFrom && s.AddedByUserId == userId);
            }
            if (searchDto.AgeTo.HasValue)
            {
                query = query.Where(s => s.Age <= searchDto.AgeTo && s.AddedByUserId == userId);
            }

            return await query.ToListAsync();
        }
    }
}

        
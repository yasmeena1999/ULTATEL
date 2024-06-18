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
                query = query.Where(s => s.FullName.Contains(searchDto.Name) && s.AddedByUserId == userId);
            }
            if (!string.IsNullOrEmpty(searchDto.Country))
            {
                query = query.Where(s => s.Country.Contains(searchDto.Country) && s.AddedByUserId == userId);
            }
            if (!string.IsNullOrEmpty(searchDto.Gender) && Enum.TryParse<Gender>(searchDto.Gender, out var genderEnum))
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

        /*   public async Task<IEnumerable<Student>> GetStudentsAsync(string userId, string search, string sort, int page, int pageSize)
        {
            var query = _context.Students.Where(s => s.UserId == userId);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search) || s.Country.Contains(search));
            }

            if (sort == "name")
            {
                query = query.OrderBy(s => s.Name);
            }
            else if (sort == "age")
            {
                query = query.OrderBy(s => s.Age);
            }
            else if (sort == "country")
            {
                query = query.OrderBy(s => s.Country);
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

         */
    
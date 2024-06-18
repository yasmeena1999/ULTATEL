using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Student>> GetStudentsAsync(string userId)
        {
            return await _context.Students.Where(s => s.AddedByUserId == userId).ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id, string userId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.AddedByUserId == userId);
        }

        public async Task AddStudentAsync(Student student)
        {
            

            
           
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteStudentAsync(int id, string userId)
        {
            var student = await GetStudentByIdAsync(id, userId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Student>> GetStudentsAsync(string userId)
        {
            return await _studentRepository.GetStudentsAsync(userId);
        }

        public async Task<Student> GetStudentByIdAsync(int id, string userId)
        {
            return await _studentRepository.GetStudentByIdAsync(id, userId);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentRepository.AddStudentAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(int id, string userId)
        {
            await _studentRepository.DeleteStudentAsync(id, userId);
        }
    }
} /*   public Student GetStudentById(int studentId, string currentUserId)
    {
        return _dbContext.Students
            .Where(s => s.Id == studentId && s.AddedByUserId == currentUserId)
            .FirstOrDefault();
    }

    // Method to add a new student, associating it with the current user
    public void AddStudent(Student student, string currentUserId)
    {
        // Assign the user who added the student
        student.AddedByUserId = currentUserId;

        // Add student to DbSet and save changes
        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();
    }

    // Method to update a student, ensuring only the user who added it can modify it
    public void UpdateStudent(Student updatedStudent, string currentUserId)
    {
        var existingStudent = _dbContext.Students.Find(updatedStudent.Id);

        // Check if the student exists and if the current user added it
        if (existingStudent != null && existingStudent.AddedByUserId == currentUserId)
        {
            existingStudent.Name = updatedStudent.Name;
            existingStudent.DateOfBirth = updatedStudent.DateOfBirth;

            _dbContext.SaveChanges();
        }
        // Handle unauthorized access or student not found scenarios as needed
    }

    // Method to delete a student, ensuring only the user who added it can delete it
    public void DeleteStudent(int studentId, string currentUserId)
    {
        var studentToDelete = _dbContext.Students.Find(studentId);

        // Check if the student exists and if the current user added it
        if (studentToDelete != null && studentToDelete.AddedByUserId == currentUserId)
        {
            _dbContext.Students.Remove(studentToDelete);
            _dbContext.SaveChanges();
        }
        // Handle unauthorized access or student not found scenarios as needed
    } */

using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;
using Services.Services.Interfaces;
using UniversityApi.Data;
using UniversityApi.Dtos;
using Microsoft.Extensions.Hosting;
using Services.Services.Extensions.UniversityApp.Service.Extentions;
using Microsoft.AspNetCore.Hosting;
using Data.Repositories.İmplementations;
using Data.Repositories.İnterfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Services.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;


        public StudentService(IStudentRepository studentRepo, IWebHostEnvironment environment,IMapper mapper,IGroupRepository groupRepository)
        {
            _studentRepo = studentRepo ;
            _environment = environment;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public int Create(CreateStudentDto createDto)
        {
            if(createDto == null)
            {
                throw new RestException(StatusCodes.Status400BadRequest,"Student is null");
            }
            Group group = _groupRepository.Get(x => x.Id == createDto.GroupId && !x.IsDeleted, "Students");

            if (group == null)
                throw new RestException(StatusCodes.Status404NotFound, "GroupId", "Group not found by given GroupId");

            if (group.Limit <= group.Students.Count)
                throw new RestException(StatusCodes.Status400BadRequest, "Group is full");

            if (group.Students.Any(x => x.Email.ToUpper() == createDto.Email.ToUpper() && !x.IsDeleted))
                throw new RestException(StatusCodes.Status400BadRequest, "Email", "Student already exists by given Email");

            var student = new Student
            {
                GroupId = createDto.GroupId,
                FullName = createDto.FullName,
                Email = createDto.Email,
                BirthDate = createDto.BirthDate,
                ImageName = createDto.FormFile.Save(_environment.WebRootPath, "students")
            };

            _studentRepo.Add(student);
            _studentRepo.Save();

            return student.Id;
        }

        public int Delete(int id)
        {
            var student = _studentRepo.Get((s => s.Id == id));

            if (student == null)
                throw new NullReferenceException($"Student with ID {id} not found.");

            _studentRepo.Delete(student);
            _studentRepo.Save();

            return student.Id;
        }

        public int Edit(int id, EditStudentDto editDto)
        {
            var student = _studentRepo.Get(s => s.Id == id);

            if (student == null)
                throw new NullReferenceException();

            student.GroupId = editDto.GroupId;
            student.FullName = editDto.FullName;
            student.Email = editDto.Email;
            student.BirthDate = editDto.BirthDate;

            _studentRepo.Save();

            return student.Id;
        }

        public List<GetStudentDto> GetAll()
        {
             List<Student> students = _studentRepo.GetAll();


            return
        }

        public GetStudentDto GetById(int id)
        {
            var student = _uniDatabase.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                throw new NullReferenceException();

            return _mapper.Map<StudentGetDto>(entity);
        }
    }
}

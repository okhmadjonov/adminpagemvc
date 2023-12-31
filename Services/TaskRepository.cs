﻿using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace AdminPageinMVC.Repository.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<List<TaskDTO>> GetAllTaskAsync()
    {
        var taskAsync = await _context.Tasks
            .Include(e => e.Lesson)
            .Select(e => new TaskDTO()
            {
                Id = e.Id,
                Description = e.Description,
                Lesson = e.Lesson,
                Title = e.Title,
                DateTime = e.DateTime,
                Process = e.Process,
            })
            .ToListAsync();
        return taskAsync;
    }

    public async Task<TaskDTO> GetTaskByIdAsync(int id)
    {
        var taskDto = await _context.Tasks
            .Include(e => e.Lesson)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var task = new TaskDTO();
        task.Id = id;
        task.Description = taskDto.Description;
        task.Lesson = taskDto.Lesson;
        task.Title = taskDto.Title;
        task.DateTime = taskDto.DateTime;
        task.Process = taskDto.Process;
        return task;
    }

    public async Task AddTaskAsync(TaskDTO taskDto)
    {
        var task = new AdminPageMVC.Entities.Task();                                           //new AdminPageinMVC.Entities.Task();
        task.Title = taskDto.Title;
        task.Description = taskDto.Description;
        task.DateTime = taskDto.DateTime;
        task.Process = taskDto.Process;
        var findLesson = await _context.Lessons.FindAsync(taskDto.Lesson.Id);
        if (findLesson != null) task.Lesson = taskDto.Lesson;
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(int id, TaskDTO taskDto)
    {
        var taskFindAsync = await _context.Tasks.FirstOrDefaultAsync(l => l.Id == id);
        taskFindAsync.Id = id;
        taskFindAsync.Title = taskDto.Title;
        taskFindAsync.Description = taskDto.Description;
        taskFindAsync.Lesson = taskDto.Lesson;
        taskFindAsync.DateTime = taskDto.DateTime;
        taskFindAsync.Process = taskDto.Process;

        _context.Entry(taskFindAsync).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
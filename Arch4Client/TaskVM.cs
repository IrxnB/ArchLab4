using Arch4Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace Arch4Client
{
    internal class TaskVM : INotifyPropertyChanged
    {
        private readonly ITaskSource _taskSource;
        private List<TaskEntity>? _tasks;
        public List<TaskEntity>? Tasks {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks)); 
            }

        }
        private TaskEntity? _taskUpdate;
        private TaskEntity? _selectedTask;
        private TaskEntity? _taskCreate;
        private ICommand _update;
        private ICommand _delete;
        private ICommand _create;
        public ICommand UpdateClick
        {
            get
            {
                return _update;
            }
            set
            {
                _update = value;
                OnPropertyChanged(nameof(UpdateClick));
            }
        }
        public ICommand DeleteClick
        {
            get
            {
                return _delete;
            }
            set
            {
                _delete = value;
                OnPropertyChanged(nameof(DeleteClick));
            }
        }
        public ICommand CreateClick
        {
            get
            {
                return _create;
            }
            set
            {
                _create = value;
                OnPropertyChanged(nameof(CreateClick));
            }
        }
        public TaskEntity? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                TaskUpdate = _selectedTask;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        public TaskEntity? TaskCreate
        {
            get => _taskCreate;
            set
            {
                _taskCreate = value;
                OnPropertyChanged(nameof(TaskCreate));
            }
        }
        public TaskEntity? TaskUpdate
        {
            get => _taskUpdate;
            set
            {
                if (value != null)
                {
                    _taskUpdate = new TaskEntity();
                    _taskUpdate.Id = value.Id;
                    _taskUpdate.Name = value.Name;
                    _taskUpdate.Description = value.Description;
                    _taskUpdate.IsCompleted = value.IsCompleted;
                    OnPropertyChanged(nameof(TaskUpdate));
                }
            }
        }
        private void initTasks()
        {
            Tasks = _taskSource.ReadAll();
            SelectedTask = Tasks.FirstOrDefault();
        }
        private void update()
        {
            _taskSource.Update(TaskUpdate);
            initTasks();
        }
        private void delete()
        {
            _taskSource.Delete(SelectedTask?.Id);
            initTasks();
        }
        private void create()
        {
            _taskSource.Create(TaskCreate);
            TaskCreate = new TaskEntity();
            initTasks();
        }
        internal TaskVM()
        {
            TaskCreate = new TaskEntity();
            UpdateClick = new CommandHandler(update);
            DeleteClick = new CommandHandler(delete);
            CreateClick = new CommandHandler(create);
            _taskSource = new TaskWebClient("https://localhost:7082/api/TaskEntities");
            initTasks();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    class CommandHandler : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action _action;

        public CommandHandler(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
}

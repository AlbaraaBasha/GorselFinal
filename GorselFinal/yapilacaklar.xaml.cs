using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GorselFinal.Model;
namespace GorselFinal
{
    public partial class yapilacaklar : ContentPage
    {
        private readonly FireBaseServices firebaseService;
        private TodoTask selectedTask = null;

        public yapilacaklar()
        {
            InitializeComponent();
            LoadTasks();
            firebaseService = new FireBaseServices();
        }


        private async void OnAddClicked(object sender, EventArgs e)
        {
           
            taskForm.IsVisible = true;
            taskListView.IsVisible = false;
            selectedTask = null;
            ClearForm();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await LoadTasks();
        }

        private async Task LoadTasks()
        {

            if (taskListView == null)
            {
                await DisplayAlert("Hata", "liste boştur.", "Tamam");
                return;
            }

            if (firebaseService == null)
            {

                return;
            }

            var tasks = await firebaseService.GetTasks();
            if (tasks != null && tasks.Any())
            {
                taskListView.ItemsSource = tasks;
            }
            else
            {
                await DisplayAlert("Hata", "liste boştur.", "Tamam");
            }
        }




        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleEntry.Text) || string.IsNullOrWhiteSpace(descriptionEntry.Text))
            {
                await DisplayAlert("Hata", "lütfen bütün boşlukları doldurun.", "Tamam");
                return;
            }

            if (selectedTask == null)
            {
                var newTask = new TodoTask
                {
                    Title = titleEntry.Text,
                    Description = descriptionEntry.Text,
                    Date = datePicker.Date.ToString("d"),
                    Time = timePicker.Time.ToString(@"hh\:mm"),
                    IsCompleted = false
                };

                await firebaseService.AddTask(newTask);
            }
            else
            {
                selectedTask.Title = titleEntry.Text;
                selectedTask.Description = descriptionEntry.Text;
                selectedTask.Date = datePicker.Date.ToString("d");
                selectedTask.Time = timePicker.Time.ToString(@"hh\:mm");

                await firebaseService.UpdateTask(selectedTask);
            }

            await DisplayAlert("Başarılı", "Görev başarıyla kaydedildi", "Tamam");

           
            taskForm.IsVisible = false;
            taskListView.IsVisible = true;

           
            await LoadTasks();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            
            taskForm.IsVisible = false;
            taskListView.IsVisible = true;
        }

        private async void OnModifyClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var task = button.BindingContext as TodoTask;

            if (task != null)
            {
                selectedTask = task;

                titleEntry.Text = task.Title;
                descriptionEntry.Text = task.Description;
                datePicker.Date = DateTime.Parse(task.Date);
                timePicker.Time = TimeSpan.Parse(task.Time);

                taskForm.IsVisible = true;
                taskListView.IsVisible = false;
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var task = button.BindingContext as TodoTask;

            if (task != null)
            {
                bool result = await DisplayAlert("Onayla", "Bu görevi silmek istediğinizden emin misiniz?", "Evet", "Hayır");

                if (result)
                {
                    await firebaseService.DeleteTask(task);
                    await DisplayAlert("Başarılı", "Görev silindi.", "Tamam");
                    
                    await LoadTasks();
                }
            }
        }

        private async void OnTaskCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkBox = sender as CheckBox;
                var task = checkBox.BindingContext as TodoTask;

                if (task != null)
                {
                    task.IsCompleted = e.Value;
                    await firebaseService.UpdateTask(task);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update task: {ex.Message}", "OK");
            }
        }

        private void ClearForm()
        {
            titleEntry.Text = string.Empty;
            descriptionEntry.Text = string.Empty;
            datePicker.Date = DateTime.Today;
            timePicker.Time = TimeSpan.Zero;
        }
    }
}


using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Jornal2
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private ObservableCollection<Subject> subjects = new ObservableCollection<Subject>();
        private ObservableCollection<Grade> grades = new ObservableCollection<Grade>();
        private ObservableCollection<Attendance> attendanceRecords = new ObservableCollection<Attendance>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            ShowStudentsPanel();
        }

        private void InitializeData()
        {
            students.Add(new Student { Id = 1, Name = "Антонов Арсений", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 2, Name = "Духина Анастасия", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 3, Name = "Землянский Денис", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 4, Name = "Золин Денис", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 5, Name = "Кубанов Альберт", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 6, Name = "Лушников Олег", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 7, Name = "Метелицен Захар", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 8, Name = "Юнусов Юсуф", Group = "Группа РПО-23/1" });


            subjects.Add(new Subject { Id = 1, Name = "Системное программирование " });
            subjects.Add(new Subject { Id = 2, Name = "Основы философии" });
            subjects.Add(new Subject { Id = 3, Name = "Упровление программными проектами" });
            subjects.Add(new Subject { Id = 3, Name = "Основы алгоритмизации и программирования" });
            subjects.Add(new Subject { Id = 3, Name = "Менеджемет в профисеональной деетельности" });

          

            dgStudents.ItemsSource = students;
            dgSubjects.ItemsSource = subjects;
            dgGrades.ItemsSource = grades;
            dgAttendance.ItemsSource = attendanceRecords;

            UpdateComboBoxes();
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentName.Text))
            {
                MessageBox.Show("Введите имя студента");
                return;
            }

            int newId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;

            
            string groupName = txtStudentGroup.Text.Trim();

     
            if (!string.IsNullOrEmpty(groupName))
            {
                if (!groupName.StartsWith("Группа", StringComparison.OrdinalIgnoreCase))
                {
                    groupName = "Группа " + groupName;
                }
            }
            else
            {
                groupName = "Группа";
            }

            students.Add(new Student
            {
                Id = newId,
                Name = txtStudentName.Text,
                Group = groupName
            });

            txtStudentName.Clear();
            txtStudentGroup.Clear();
            UpdateComboBoxes();
        }

       

       
        private void TxtStudentGroup_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && !char.IsLetter(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void UpdateComboBoxes()
        {
            cbStudentForGrade.ItemsSource = students;
            cbStudentForAttendance.ItemsSource = students;
            cbSubjectForGrade.ItemsSource = subjects;
            cbSubjectForAttendance.ItemsSource = subjects;
        }

        private void ShowStudentsPanel()
        {
            studentsPanel.Visibility = Visibility.Visible;
            subjectsPanel.Visibility = Visibility.Collapsed;
            gradesPanel.Visibility = Visibility.Collapsed;
            attendancePanel.Visibility = Visibility.Collapsed;
        }

        private void ShowSubjectsPanel()
        {
            studentsPanel.Visibility = Visibility.Collapsed;
            subjectsPanel.Visibility = Visibility.Visible;
            gradesPanel.Visibility = Visibility.Collapsed;
            attendancePanel.Visibility = Visibility.Collapsed;
        }

        private void ShowGradesPanel()
        {
            studentsPanel.Visibility = Visibility.Collapsed;
            subjectsPanel.Visibility = Visibility.Collapsed;
            gradesPanel.Visibility = Visibility.Visible;
            attendancePanel.Visibility = Visibility.Collapsed;
        }

        private void ShowAttendancePanel()
        {
            studentsPanel.Visibility = Visibility.Collapsed;
            subjectsPanel.Visibility = Visibility.Collapsed;
            gradesPanel.Visibility = Visibility.Collapsed;
            attendancePanel.Visibility = Visibility.Visible;
        }

     
        private void BtnStudents_Click(object sender, RoutedEventArgs e) => ShowStudentsPanel();
        private void BtnSubjects_Click(object sender, RoutedEventArgs e) => ShowSubjectsPanel();
        private void BtnGrades_Click(object sender, RoutedEventArgs e) => ShowGradesPanel();
        private void BtnAttendance_Click(object sender, RoutedEventArgs e) => ShowAttendancePanel();

        
        private void BtAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentName.Text))
            {
                MessageBox.Show("Введите имя студента");
                return;
            }

            int newId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
            students.Add(new Student
            {
                Id = newId,
                Name = txtStudentName.Text,
                Group = txtStudentGroup.Text
            });

            txtStudentName.Clear();
            txtStudentGroup.Clear();
            UpdateComboBoxes();
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is Student student)
            {
           
                var gradesToRemove = grades.Where(g => g.StudentId == student.Id).ToList();
                foreach (var grade in gradesToRemove)
                {
                    grades.Remove(grade);
                }

                var attendanceToRemove = attendanceRecords.Where(a => a.StudentId == student.Id).ToList();
                foreach (var attendance in attendanceToRemove)
                {
                    attendanceRecords.Remove(attendance);
                }

                students.Remove(student);
                UpdateComboBoxes();
            }
        }

      
        private void BtnAddSubject_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                MessageBox.Show("Введите название предмета");
                return;
            }

            int newId = subjects.Count > 0 ? subjects.Max(s => s.Id) + 1 : 1;
            subjects.Add(new Subject
            {
                Id = newId,
                Name = txtSubjectName.Text
            });

            txtSubjectName.Clear();
            UpdateComboBoxes();
        }

        private void DeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            if (dgSubjects.SelectedItem is Subject subject)
            {
       
                var gradesToRemove = grades.Where(g => g.SubjectId == subject.Id).ToList();
                foreach (var grade in gradesToRemove)
                {
                    grades.Remove(grade);
                }

                var attendanceToRemove = attendanceRecords.Where(a => a.SubjectId == subject.Id).ToList();
                foreach (var attendance in attendanceToRemove)
                {
                    attendanceRecords.Remove(attendance);
                }

                subjects.Remove(subject);
                UpdateComboBoxes();
            }
        }

        private void BtnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            if (cbStudentForGrade.SelectedItem == null || cbSubjectForGrade.SelectedItem == null)
            {
                MessageBox.Show("Выберите студента и предмет");
                return;
            }

            if (cbGradeValue.SelectedItem == null)
            {
                MessageBox.Show("Выберите оценку");
                return;
            }

            var student = cbStudentForGrade.SelectedItem as Student;
            var subject = cbSubjectForGrade.SelectedItem as Subject;
            int gradeValue = int.Parse((cbGradeValue.SelectedItem as ComboBoxItem).Content.ToString());

            int newId = grades.Count > 0 ? grades.Max(g => g.Id) + 1 : 1;

            grades.Add(new Grade
            {
                Id = newId,
                StudentId = student.Id,
                StudentName = student.Name,
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                Value = gradeValue,
                Date = DateTime.Now
            });
        }

        private void DeleteGrade_Click(object sender, RoutedEventArgs e)
        {
            if (dgGrades.SelectedItem is Grade grade)
            {
                grades.Remove(grade);
            }
        }

        
        private void BtnAddAttendance_Click(object sender, RoutedEventArgs e)
        {
            if (cbStudentForAttendance.SelectedItem == null || cbSubjectForAttendance.SelectedItem == null)
            {
                MessageBox.Show("Выберите студента и предмет");
                return;
            }

            if (dpAttendanceDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }

            var student = cbStudentForAttendance.SelectedItem as Student;
            var subject = cbSubjectForAttendance.SelectedItem as Subject;

            int newId = attendanceRecords.Count > 0 ? attendanceRecords.Max(a => a.Id) + 1 : 1;

            var attendance = new Attendance
            {
                Id = newId,
                StudentId = student.Id,
                StudentName = student.Name,
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                Date = dpAttendanceDate.SelectedDate.Value,
                IsPresent = cbIsPresent.IsChecked ?? false
            };

            attendanceRecords.Add(attendance);
            UpdateAttendanceStats(student.Id, subject.Id);
        }

        private void DeleteAttendance_Click(object sender, RoutedEventArgs e)
        {
            if (dgAttendance.SelectedItem is Attendance attendance)
            {
                attendanceRecords.Remove(attendance);
            }
        }

        private void UpdateAttendanceStats(int studentId, int subjectId)
        {
            var studentRecords = attendanceRecords
                .Where(a => a.StudentId == studentId && a.SubjectId == subjectId)
                .ToList();

            if (studentRecords.Any())
            {
                int total = studentRecords.Count;
                int present = studentRecords.Count(a => a.IsPresent);
                double percentage = (double)present / total * 100;

                tbAttendanceStats.Text = $"Всего занятий: {total}, Присутствовал: {present}, " +
                                       $"Процент посещаемости: {percentage:F1}%";
            }
        }
    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }

    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
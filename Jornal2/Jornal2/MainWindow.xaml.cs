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
        private ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();
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
            // Добавляем студентов
            students.Add(new Student { Id = 1, Name = "Антонов Арсений", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 2, Name = "Духина Анастасия", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 3, Name = "Землянский Денис", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 4, Name = "Золин Денис", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 5, Name = "Кубанов Альберт", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 6, Name = "Лушников Олег", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 7, Name = "Метелицен Захар", Group = "Группа РПО-23/1" });
            students.Add(new Student { Id = 8, Name = "Юнусов Юсуф", Group = "Группа РПО-23/1" });

            // Добавляем преподавателей
            teachers.Add(new Teacher { Id = 1, Name = "Зимен Павел Василевич", Position = "Преподователь информатики" });
            teachers.Add(new Teacher { Id = 2, Name = "Савенко Лидия Алексеевна ", Position = "РПО" });
            teachers.Add(new Teacher { Id = 3, Name = "Микулов Антон Владимирович", Position = "Основы философии" });

            // Добавляем предметы (исправлены ID)
            subjects.Add(new Subject { Id = 1, Name = "Системное программирование", TeacherId = 1, TeacherName = "Зимен Павел Василевич" });
            subjects.Add(new Subject { Id = 2, Name = "Основы философии", TeacherId = 2, TeacherName = "Микулов Антон Владимирович" });
            subjects.Add(new Subject { Id = 3, Name = "Управление программными проектами", TeacherId = 3, TeacherName = "Зимен Павел Василевич" });
            subjects.Add(new Subject { Id = 4, Name = "Основы алгоритмизации и программирования", TeacherId = 1, TeacherName = "Зимен Павел Василевич" });
            subjects.Add(new Subject { Id = 5, Name = "Менеджмент в профессиональной деятельности", TeacherId = 2, TeacherName = "Савенко Лидия Алексеевна" });

            // Привязка данных
            dgStudents.ItemsSource = students;
            dgTeachers.ItemsSource = teachers;
            dgSubjects.ItemsSource = subjects;
            dgGrades.ItemsSource = grades;
            dgAttendance.ItemsSource = attendanceRecords;

            // Обновление ComboBox
            UpdateComboBoxes();
        }

        private void UpdateComboBoxes()
        {
            cbStudentForGrade.ItemsSource = students;
            cbStudentForAttendance.ItemsSource = students;
            cbSubjectForGrade.ItemsSource = subjects;
            cbSubjectForAttendance.ItemsSource = subjects;
            cbTeacherForSubject.ItemsSource = teachers;
        }

        // Методы навигации
        private void ShowStudentsPanel()
        {
            studentsPanel.Visibility = Visibility.Visible;
            teachersPanel.Visibility = Visibility.Collapsed;
            subjectsPanel.Visibility = Visibility.Collapsed;
            gradesPanel.Visibility = Visibility.Collapsed;
            attendancePanel.Visibility = Visibility.Collapsed;
        }

        private void ShowTeachersPanel()
        {
            studentsPanel.Visibility = Visibility.Collapsed;
            teachersPanel.Visibility = Visibility.Visible;
            subjectsPanel.Visibility = Visibility.Collapsed;
            gradesPanel.Visibility = Visibility.Collapsed;
            attendancePanel.Visibility = Visibility.Collapsed;
        }

        private void ShowSubjectsPanel()
        {
            studentsPanel.Visibility = Visibility.Collapsed;
            teachersPanel.Visibility = Visibility.Collapsed;
            subjectsPanel.Visibility = Visibility.Visible;
            gradesPanel.Visibility = Visibility.Collapsed;
            attendancePanel.Visibility = Visibility.Collapsed;
        }

        private void ShowGradesPanel()
        {
            studentsPanel.Visibility = Visibility.Collapsed;
            teachersPanel.Visibility = Visibility.Collapsed;
            subjectsPanel.Visibility = Visibility.Collapsed;
            gradesPanel.Visibility = Visibility.Visible;
            attendancePanel.Visibility = Visibility.Collapsed;
        }

        private void ShowAttendancePanel()
        {
            studentsPanel.Visibility = Visibility.Collapsed;
            teachersPanel.Visibility = Visibility.Collapsed;
            subjectsPanel.Visibility = Visibility.Collapsed;
            gradesPanel.Visibility = Visibility.Collapsed;
            attendancePanel.Visibility = Visibility.Visible;
        }

        // Обработчики кнопок навигации
        private void BtnStudents_Click(object sender, RoutedEventArgs e) => ShowStudentsPanel();
        private void BtnTeachers_Click(object sender, RoutedEventArgs e) => ShowTeachersPanel();
        private void BtnSubjects_Click(object sender, RoutedEventArgs e) => ShowSubjectsPanel();
        private void BtnGrades_Click(object sender, RoutedEventArgs e) => ShowGradesPanel();
        private void BtnAttendance_Click(object sender, RoutedEventArgs e) => ShowAttendancePanel();

        // Добавление студента
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

        // Удаление студента
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

        // Добавление преподавателя
        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTeacherName.Text))
            {
                MessageBox.Show("Введите имя преподавателя");
                return;
            }

            int newId = teachers.Count > 0 ? teachers.Max(t => t.Id) + 1 : 1;

            teachers.Add(new Teacher
            {
                Id = newId,
                Name = txtTeacherName.Text,
                Position = txtTeacherPosition.Text
            });

            txtTeacherName.Clear();
            txtTeacherPosition.Clear();
            UpdateComboBoxes();
        }

        // Удаление преподавателя
        private void DeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (dgTeachers.SelectedItem is Teacher teacher)
            {
                // Удаляем предметы, которые вел этот преподаватель
                var subjectsToRemove = subjects.Where(s => s.TeacherId == teacher.Id).ToList();
                foreach (var subject in subjectsToRemove)
                {
                    // Удаляем оценки и посещаемость по этим предметам
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
                }

                teachers.Remove(teacher);
                UpdateComboBoxes();
            }
        }

        // Добавление предмета
        private void BtnAddSubject_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                MessageBox.Show("Введите название предмета");
                return;
            }

            if (cbTeacherForSubject.SelectedItem == null)
            {
                MessageBox.Show("Выберите преподавателя");
                return;
            }

            int newId = subjects.Count > 0 ? subjects.Max(s => s.Id) + 1 : 1;
            var teacher = cbTeacherForSubject.SelectedItem as Teacher;

            subjects.Add(new Subject
            {
                Id = newId,
                Name = txtSubjectName.Text,
                TeacherId = teacher.Id,
                TeacherName = teacher.Name
            });

            txtSubjectName.Clear();
            cbTeacherForSubject.SelectedIndex = -1;
            UpdateComboBoxes();
        }

        // Удаление предмета
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

        // Добавление оценки
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

        // Удаление оценки
        private void DeleteGrade_Click(object sender, RoutedEventArgs e)
        {
            if (dgGrades.SelectedItem is Grade grade)
            {
                grades.Remove(grade);
            }
        }

        // Добавление посещаемости
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

        // Удаление посещаемости
        private void DeleteAttendance_Click(object sender, RoutedEventArgs e)
        {
            if (dgAttendance.SelectedItem is Attendance attendance)
            {
                attendanceRecords.Remove(attendance);
            }
        }

        // Обновление статистики посещаемости
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

        // Валидация ввода группы
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
    }

    // Классы моделей
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
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
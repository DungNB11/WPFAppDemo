using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WPFAppDemo
{
    /// <summary>
    /// Interaction logic for FrmStudent.xaml
    /// </summary>
    public partial class FrmStudent : Window
    {
        private List<Student> students =null;
        public FrmStudent()
        {
            InitializeComponent();
        }
        private void txtCode_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCode.Background = Brushes.AntiqueWhite;
        }

        private void txtCode_LostFocus(object sender, RoutedEventArgs e)
        {
            txtCode.Background = Brushes.White;
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            txtName.Background = Brushes.White;
        }

        private void txtMark_LostFocus(object sender, RoutedEventArgs e)
        {
            txtMark.Background = Brushes.White;
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtName.Background = Brushes.AntiqueWhite;
        }

        private void txtMark_GotFocus(object sender, RoutedEventArgs e)
        {
            txtMark.Background = Brushes.AntiqueWhite;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Add thong tin cua mot student vao database
            //Hien thi thong tin tren listbox
            Student newStudent = getStudent();
            if (newStudent != null)
            {
                bool isExist = students.Any(s => s.Code.Equals(newStudent.Code));
                if (isExist)
                {
                    MessageBox.Show("Code cannot be duplicated!", "Notification");
                }
                else
                {
                    students.Add(newStudent);
                    lstStudent.Items.Add(newStudent);
                    MessageBox.Show("Added successfully!", "Notification");
                }
            }
            else
            {
                MessageBox.Show("Add new failed!", "Notification");
            }
        }

        private Student getStudent()
        {
            Student s = null;
            try
            {
                string code = txtCode.Text;
                string name = txtName.Text;
                string subject = cboSubject.Text;
                int mark = Int32.Parse(txtMark.Text);
                s = new Student(code, name, subject, mark);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            return s;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(students != null)
            {
                Student selectedStudent = (Student)lstStudent.SelectedItem;
                if (selectedStudent != null)
                {
                    students.Remove(selectedStudent);
                    lstStudent.ItemsSource = null;
                    lstStudent.ItemsSource = students;

                    MessageBox.Show("Deleted successfully!", "Notification");
                }
                else
                {
                    MessageBox.Show("Please select a student to delete.", "Notification");
                }
            }else
            {
                MessageBox.Show("List student EMPTY.", "Notification");
            }
            
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"..\..\..\Resource\students.txt";
            string fullPath = Path.GetFullPath(filePath);
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                students = new List<Student>();

                foreach (var line in lines)
                {
                    string[] fields = line.Split('\t');
                    if (fields.Length == 4)
                    {
                        string code = fields[0];
                        string name = fields[1];
                        string subject = fields[2];
                        int mark;
                        if (int.TryParse(fields[3], out mark))
                        {
                            students.Add(new Student(code, name, subject, mark));
                            
                        }
                    }
                }

                lstStudent.ItemsSource = null;
                lstStudent.ItemsSource = students;

                MessageBox.Show("Data loaded successfully from file!", "Notification");
            }
            else
            {
                MessageBox.Show("File not found!", "Error");
            }
        }

        private void WriteStudentsToFile(string filePath, List<Student> students)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (Student student in students)
                    {
                        writer.WriteLine(student.ToString());
                    }
                }

                Console.WriteLine("Successfully wrote the students to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"..\..\..\Resource\students.txt";
            string fullPath = Path.GetFullPath(filePath);
            WriteStudentsToFile(fullPath, students);
        }
    }
}

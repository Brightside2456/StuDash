
using System.ComponentModel;
using System.Windows.Forms;

namespace StuDash
{
    public partial class MainForm : Form
    {
        private StudentService _studentService;
        private Student _currentStudent;
        private bool _isEditing;

        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // Initialize service
            _studentService = new StudentService();

            // Configure DataGridView
            ConfigureDataGridView();

            // Populate ComboBoxes
            PopulateComboBoxes();

            // Load initial data
            RefreshStudentList();

            // Clear form
            ClearForm();

            // Set initial UI state
            SetUIState(false);
        }

        #region DataGridView Configuration

        private void ConfigureDataGridView()
        {
            // Clear existing columns
            dgvStudents.Columns.Clear();

            // Configure basic properties
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;

            // Add columns
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentID",
                HeaderText = "Student ID",
                DataPropertyName = "StudentID",
                Width = 120,
                ReadOnly = true
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                HeaderText = "Full Name",
                DataPropertyName = "FullName",
                Width = 150,
                ReadOnly = true
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 180,
                ReadOnly = true
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Course",
                HeaderText = "Course",
                DataPropertyName = "Course",
                Width = 130,
                ReadOnly = true
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Faculty",
                HeaderText = "Faculty",
                DataPropertyName = "Faculty",
                Width = 120,
                ReadOnly = true
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "YearLevel",
                HeaderText = "Year",
                DataPropertyName = "YearLevel",
                Width = 100,
                ReadOnly = true
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CWA",
                HeaderText = "CWA",
                DataPropertyName = "CWA",
                Width = 70,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });

            // Handle selection change
            dgvStudents.SelectionChanged += DgvStudents_SelectionChanged;
        }

        #endregion

        #region ComboBox Population

        private void PopulateComboBoxes()
        {
            // Faculty ComboBox
            cmbFaculty.Items.Clear();
            cmbFaculty.Items.AddRange(new string[]
            {
                "Engineering",
                "Science",
                "Arts & Humanities",
                "Business & Economics",
                "Medicine & Health Sciences",
                "Social Sciences",
                "Law",
                "Education"
            });

            // Year Level ComboBox
            cmbYearLevel.Items.Clear();
            cmbYearLevel.Items.AddRange(new string[]
            {
                "First Year",
                "Second Year",
                "Third Year",
                "Fourth Year",
                "Fifth Year"
            });

            // Handle faculty change to update departments
            cmbFaculty.SelectedIndexChanged += CmbFaculty_SelectedIndexChanged;
        }

        private void CmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update department based on faculty selection
            cmbDepartment.Items.Clear();

            switch (cmbFaculty.SelectedItem?.ToString())
            {
                case "Engineering":
                    cmbDepartment.Items.AddRange(new string[]
                    {
                        "Computer Science & Engineering",
                        "Electrical Engineering",
                        "Mechanical Engineering",
                        "Civil Engineering",
                        "Chemical Engineering"
                    });
                    break;

                case "Science":
                    cmbDepartment.Items.AddRange(new string[]
                    {
                        "Mathematics",
                        "Physics",
                        "Chemistry",
                        "Biology",
                        "Statistics"
                    });
                    break;

                case "Business & Economics":
                    cmbDepartment.Items.AddRange(new string[]
                    {
                        "Business Administration",
                        "Economics",
                        "Accounting",
                        "Marketing",
                        "Finance"
                    });
                    break;

                default:
                    cmbDepartment.Items.Add("General");
                    break;
            }

            // Update course based on department
            cmbDepartment.SelectedIndexChanged += (s, args) => UpdateCourseOptions();
        }

        private void UpdateCourseOptions()
        {
            cmbCourse.Items.Clear();

            string department = cmbDepartment.SelectedItem?.ToString();

            switch (department)
            {
                case "Computer Science & Engineering":
                    cmbCourse.Items.AddRange(new string[]
                    {
                        "Computer Science",
                        "Software Engineering",
                        "Information Technology",
                        "Computer Engineering"
                    });
                    break;

                case "Business Administration":
                    cmbCourse.Items.AddRange(new string[]
                    {
                        "Bachelor of Business Administration",
                        "Master of Business Administration",
                        "Human Resource Management"
                    });
                    break;

                default:
                    // Add generic course based on department name
                    if (!string.IsNullOrEmpty(department))
                    {
                        cmbCourse.Items.Add($"Bachelor of {department}");
                        cmbCourse.Items.Add($"Master of {department}");
                    }
                    break;
            }
        }

        #endregion

        #region Data Operations

        private void RefreshStudentList()
        {
            try
            {
                var students = _studentService.GetAllStudents();
                dgvStudents.DataSource = new BindingList<Student>(students);

                // Update status
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusBar()
        {
            int totalStudents = _studentService.GetTotalStudentCount();
            // Assuming your status strip has labels named toolStripStatusLabel1 and toolStripStatusLabel2
            // Update these names to match your actual status strip labels
            if (statusStrip1.Items.Count > 0)
            {
                statusStrip1.Items[0].Text = "Ready";
            }
            if (statusStrip1.Items.Count > 1)
            {
                statusStrip1.Items[1].Text = $"Total Students: {totalStudents}";
            }
        }

        #endregion

        #region Event Handlers

        private void DgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var selectedStudent = dgvStudents.SelectedRows[0].DataBoundItem as Student;
                if (selectedStudent != null)
                {
                    LoadStudentToForm(selectedStudent);
                    SetUIState(true); // Enable edit/delete buttons
                }
            }
            else
            {
                SetUIState(false); // Disable edit/delete buttons
            }
        }

        private void BtnAddStudent_Click(object sender, EventArgs e)
        {
            _isEditing = false;
            _currentStudent = new Student();
            ClearForm();
            SetFormEnabled(true);
            txtStudentID.Focus();

            // Switch to Personal Info tab
            tabControlStudent.SelectedIndex = 0;
        }

        private void BtnEditStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to edit.", "No Selection",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isEditing = true;
            SetFormEnabled(true);
            txtStudentID.Focus();

            // Switch to Personal Info tab
            tabControlStudent.SelectedIndex = 0;
        }

        private void BtnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to delete.", "No Selection",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedStudent = dgvStudents.SelectedRows[0].DataBoundItem as Student;
            if (selectedStudent != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete student {selectedStudent.FullName} ({selectedStudent.StudentID})?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _studentService.DeleteStudent(selectedStudent.ID);
                        RefreshStudentList();
                        ClearForm();
                        MessageBox.Show("Student deleted successfully!", "Success",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting student: {ex.Message}", "Error",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();
                var searchResults = _studentService.SearchStudents(searchTerm);
                dgvStudents.DataSource = new BindingList<Student>(searchResults);

                // Update status to show search results
                if (statusStrip1.Items.Count > 1)
                {
                    if (string.IsNullOrEmpty(searchTerm))
                    {
                        statusStrip1.Items[1].Text = $"Total Students: {searchResults.Count}";
                    }
                    else
                    {
                        statusStrip1.Items[1].Text = $"Search Results: {searchResults.Count}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching students: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveStudent_Click(object sender, EventArgs e)
        {
            //if (ValidateForm())
            if (true)
            {
                try
                {
                    // Get data from form
                    PopulateStudentFromForm();

                    if (_isEditing)
                    {
                        _studentService.UpdateStudent(_currentStudent);
                        MessageBox.Show("Student updated successfully!", "Success",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _studentService.AddStudent(_currentStudent);
                        MessageBox.Show("Student added successfully!", "Success",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    RefreshStudentList();
                    ClearForm();
                    SetFormEnabled(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving student: {ex.Message}", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClearForm_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetFormEnabled(false);
            _isEditing = false;
            _currentStudent = null;
        }

        // Handle Enter key in search textbox
        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        #endregion

        #region Form Management

        private void LoadStudentToForm(Student student)
        {
            if (student == null) return;

            _currentStudent = student;

            // Personal Info Tab
            txtStudentID.Text = student.StudentID;
            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            dtpDateOfBirth.Value = student.DateOfBirth;
            txtEmail.Text = student.Email;
            txtPhone.Text = student.PhoneNumber;

            // Academic Info Tab
            cmbFaculty.SelectedItem = student.Faculty;
            cmbDepartment.SelectedItem = student.Department;
            cmbCourse.SelectedItem = student.Course;
            cmbYearLevel.SelectedItem = student.YearLevel;
            nudCWA.Value = (decimal)student.CWA;
            dtpEnrollmentDate.Value = student.EnrollmentDate;
        }

        private void PopulateStudentFromForm()
        {
            if (_currentStudent == null)
                _currentStudent = new Student();

            _currentStudent.StudentID = txtStudentID.Text.Trim();
            _currentStudent.FirstName = txtFirstName.Text.Trim();
            _currentStudent.LastName = txtLastName.Text.Trim();
            _currentStudent.DateOfBirth = dtpDateOfBirth.Value;
            _currentStudent.Email = txtEmail.Text.Trim();
            _currentStudent.PhoneNumber = txtPhone.Text.Trim();
            _currentStudent.Faculty = cmbFaculty.SelectedItem?.ToString() ?? "";
            _currentStudent.Department = cmbDepartment.SelectedItem?.ToString() ?? "";
            _currentStudent.Course = cmbCourse.SelectedItem?.ToString() ?? "";
            _currentStudent.YearLevel = cmbYearLevel.SelectedItem?.ToString() ?? "";
            _currentStudent.CWA = (double)nudCWA.Value;
            _currentStudent.EnrollmentDate = dtpEnrollmentDate.Value;
        }

        private void ClearForm()
        {
            // Clear Personal Info
            txtStudentID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-18);
            txtEmail.Clear();
            txtPhone.Clear();

            // Clear Academic Info
            cmbFaculty.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            cmbCourse.SelectedIndex = -1;
            cmbYearLevel.SelectedIndex = -1;
            nudCWA.Value = 0;
            dtpEnrollmentDate.Value = DateTime.Now;

            // Clear selection in DataGridView
            dgvStudents.ClearSelection();

            _currentStudent = null;
        }

        private void SetFormEnabled(bool enabled)
        {
            // Personal Info Tab
            txtStudentID.Enabled = enabled;
            txtFirstName.Enabled = enabled;
            txtLastName.Enabled = enabled;
            dtpDateOfBirth.Enabled = enabled;
            txtEmail.Enabled = enabled;
            txtPhone.Enabled = enabled;

            // Academic Info Tab
            cmbFaculty.Enabled = enabled;
            cmbDepartment.Enabled = enabled;
            cmbCourse.Enabled = enabled;
            cmbYearLevel.Enabled = enabled;
            nudCWA.Enabled = enabled;
            dtpEnrollmentDate.Enabled = enabled;

            // Action Buttons
            btnSaveStudent.Enabled = enabled;
            btnClearForm.Enabled = enabled;
            btnCancel.Enabled = enabled;
        }

        private void SetUIState(bool hasSelection)
        {
            btnEditStudent.Enabled = hasSelection;
            btnDeleteStudent.Enabled = hasSelection;
        }

        //private bool ValidateForm()
        //{
            //// Clear any previous error messages
            //errorProvider.Clear();

            //bool isValid = true;

            //// Validate Student ID
            //if (string.IsNullOrWhiteSpace(txtStudentID.Text))
            //{
            //    errorProvider.SetError(txtStudentID, "Student ID is required");
            //    isValid = false;
            //}

            //// Validate First Name
            //if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            //{
            //    errorProvider.SetError(txtFirstName, "First name is required");
            //    isValid = false;
            //}

            //// Validate Last Name
            //if (string.IsNullOrWhiteSpace(txtLastName.Text))
            //{
            //    errorProvider.SetError(txtLastName, "Last name is required");
            //    isValid = false;
            //}

            //// Validate Email
            //if (string.IsNullOrWhiteSpace(txtEmail.Text))
            //{
            //    errorProvider.SetError(txtEmail, "Email is required");
            //    isValid = false;
            //}
            //else if (!IsValidEmail(txtEmail.Text))
            //{
            //    errorProvider.SetError(txtEmail, "Please enter a valid email address");
            //    isValid = false;
            //}

            //// Validate Date of Birth (must be at least 16 years old)
            //if (dtpDateOfBirth.Value >= DateTime.Now.AddYears(-16))
            //{
            //    errorProvider.SetError(dtpDateOfBirth, "Student must be at least 16 years old");
            //    isValid = false;
            //}

            //// Validate Faculty
            //if (cmbFaculty.SelectedIndex == -1)
            //{
            //    errorProvider.SetError(cmbFaculty, "Faculty is required");
            //    isValid = false;
            //}

            //// Validate Department
            //if (cmbDepartment.SelectedIndex == -1)
            //{
            //    errorProvider.SetError(cmbDepartment, "Department is required");
            //    isValid = false;
            //}

            //// Validate Course
            //if (cmbCourse.SelectedIndex == -1)
            //{
            //    errorProvider.SetError(cmbCourse, "Course is required");
            //    isValid = false;
            //}

            //// Validate Year Level
            //if (cmbYearLevel.SelectedIndex == -1)
            //{
            //    errorProvider.SetError(cmbYearLevel, "Year level is required");
            //    isValid = false;
            //}

            //// Validate Enrollment Date (cannot be in the future)
            //if (dtpEnrollmentDate.Value > DateTime.Now)
            //{
            //    errorProvider.SetError(dtpEnrollmentDate, "Enrollment date cannot be in the future");
            //    isValid = false;
            //}

            //// Check for duplicate Student ID (only when adding new or changing ID)
            //if (!_isEditing || (_currentStudent != null && _currentStudent.StudentID != txtStudentID.Text))
            //{
            //    var existingStudent = _studentService.GetStudentByStudentId(txtStudentID.Text.Trim());
            //    if (existingStudent != null)
            //    {
            //        errorProvider.SetError(txtStudentID, "Student ID already exists");
            //        isValid = false;
            //    }
            //}

            //if (!isValid)
            //{
            //    MessageBox.Show("Please correct the highlighted errors before saving.", "Validation Error",
            //                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //return isValid;
        //}

        //private bool IsValidEmail(string email)
        //{
        //    try
        //    {
        //        var addr = new System.Net.Mail.MailAddress(email);
        //        return addr.Address == email;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        #endregion

        #region Additional Features

        // Method to export student data (you can add this later)
        private void ExportToCSV()
        {
            // Implementation for exporting student data to CSV
            // This can be added as an enhancement
        }

        // Method to import student data (you can add this later)
        private void ImportFromCSV()
        {
            // Implementation for importing student data from CSV
            // This can be added as an enhancement
        }

        #endregion

        #region Form Events

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set initial focus
            txtSearch.Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Confirm if user wants to exit when there are unsaved changes
            if (btnSaveStudent.Enabled)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to exit?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        #endregion
    
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

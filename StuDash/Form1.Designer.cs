namespace StuDash
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            btnDeleteStudent = new Button();
            btnEditStudent = new Button();
            btnAddStudent = new Button();
            txtSearch = new TextBox();
            splitContainer1 = new SplitContainer();
            statusStrip1 = new StatusStrip();
            dgvStudents = new DataGridView();
            groupBoxDetails = new GroupBox();
            btnCancel = new Button();
            btnClearForm = new Button();
            btnSaveStudent = new Button();
            tabControlStudent = new TabControl();
            tabPersonal = new TabPage();
            dtpDateOfBirth = new DateTimePicker();
            txtPhone = new TextBox();
            txtEmail = new TextBox();
            txtLastName = new TextBox();
            txtFirstName = new TextBox();
            lblPhone = new Label();
            lblEmail = new Label();
            lblDateOfBirth = new Label();
            lblLastName = new Label();
            lblFirstName = new Label();
            txtStudentID = new TextBox();
            lblStudentID = new Label();
            tabAcademic = new TabPage();
            dtpEnrollmentDate = new DateTimePicker();
            nudCWA = new NumericUpDown();
            cmbDepartment = new ComboBox();
            cmbCourse = new ComboBox();
            cmbYearLevel = new ComboBox();
            cmbFaculty = new ComboBox();
            lblEnrolmentDate = new Label();
            lblCWA = new Label();
            lblYearLevel = new Label();
            lblCourse = new Label();
            lblDepartment = new Label();
            lblFaculty = new Label();
            tabSummary = new TabPage();
            rtbSummary = new RichTextBox();
            errorProvider = new ErrorProvider(components);
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            groupBoxDetails.SuspendLayout();
            tabControlStudent.SuspendLayout();
            tabPersonal.SuspendLayout();
            tabAcademic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCWA).BeginInit();
            tabSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(btnDeleteStudent);
            panel1.Controls.Add(btnEditStudent);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(982, 60);
            panel1.TabIndex = 0;
            // 
            // btnDeleteStudent
            // 
            btnDeleteStudent.Location = new Point(234, 13);
            btnDeleteStudent.Name = "btnDeleteStudent";
            btnDeleteStudent.Size = new Size(80, 35);
            btnDeleteStudent.TabIndex = 3;
            btnDeleteStudent.Text = "Delete";
            btnDeleteStudent.UseVisualStyleBackColor = true;
            btnDeleteStudent.Click += BtnDeleteStudent_Click;
            // 
            // btnEditStudent
            // 
            btnEditStudent.BackColor = Color.SteelBlue;
            btnEditStudent.Location = new Point(148, 12);
            btnEditStudent.Name = "btnEditStudent";
            btnEditStudent.Size = new Size(80, 35);
            btnEditStudent.TabIndex = 2;
            btnEditStudent.Text = "Edit";
            btnEditStudent.UseVisualStyleBackColor = false;
            btnEditStudent.Click += BtnEditStudent_Click;
            // 
            // btnAddStudent
            // 
            btnAddStudent.BackColor = Color.Green;
            btnAddStudent.FlatStyle = FlatStyle.Flat;
            btnAddStudent.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddStudent.ForeColor = Color.White;
            btnAddStudent.Location = new Point(10, 12);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(132, 35);
            btnAddStudent.TabIndex = 1;
            btnAddStudent.Text = "Add Student";
            btnAddStudent.UseVisualStyleBackColor = false;
            btnAddStudent.Click += BtnAddStudent_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(320, 17);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 27);
            txtSearch.TabIndex = 2;
            txtSearch.Click += BtnSearch_Click;
            txtSearch.KeyPress += TxtSearch_KeyPress;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 60);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(statusStrip1);
            splitContainer1.Panel1.Controls.Add(dgvStudents);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBoxDetails);
            splitContainer1.Size = new Size(982, 593);
            splitContainer1.SplitterDistance = 600;
            splitContainer1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 567);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(600, 26);
            statusStrip1.TabIndex = 1;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.BorderStyle = BorderStyle.None;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.Location = new Point(0, 0);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(600, 593);
            dgvStudents.TabIndex = 0;
            // 
            // groupBoxDetails
            // 
            groupBoxDetails.Controls.Add(btnCancel);
            groupBoxDetails.Controls.Add(btnClearForm);
            groupBoxDetails.Controls.Add(btnSaveStudent);
            groupBoxDetails.Controls.Add(tabControlStudent);
            groupBoxDetails.Dock = DockStyle.Fill;
            groupBoxDetails.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxDetails.Location = new Point(0, 0);
            groupBoxDetails.Name = "groupBoxDetails";
            groupBoxDetails.Padding = new Padding(10, 10, 10, 120);
            groupBoxDetails.Size = new Size(378, 593);
            groupBoxDetails.TabIndex = 0;
            groupBoxDetails.TabStop = false;
            groupBoxDetails.Text = "Student Details";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Gray;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(270, 484);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 35);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnClearForm
            // 
            btnClearForm.BackColor = Color.Orange;
            btnClearForm.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearForm.ForeColor = Color.White;
            btnClearForm.Location = new Point(160, 484);
            btnClearForm.Name = "btnClearForm";
            btnClearForm.Size = new Size(90, 35);
            btnClearForm.TabIndex = 2;
            btnClearForm.Text = "Clear Form";
            btnClearForm.UseVisualStyleBackColor = false;
            btnClearForm.Click += BtnClearForm_Click;
            // 
            // btnSaveStudent
            // 
            btnSaveStudent.BackColor = Color.Green;
            btnSaveStudent.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveStudent.ForeColor = Color.White;
            btnSaveStudent.Location = new Point(29, 484);
            btnSaveStudent.Name = "btnSaveStudent";
            btnSaveStudent.Size = new Size(114, 35);
            btnSaveStudent.TabIndex = 1;
            btnSaveStudent.Text = "Save Student";
            btnSaveStudent.UseVisualStyleBackColor = false;
            btnSaveStudent.Click += BtnSaveStudent_Click;
            // 
            // tabControlStudent
            // 
            tabControlStudent.Controls.Add(tabPersonal);
            tabControlStudent.Controls.Add(tabAcademic);
            tabControlStudent.Controls.Add(tabSummary);
            tabControlStudent.Dock = DockStyle.Fill;
            tabControlStudent.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControlStudent.Location = new Point(10, 33);
            tabControlStudent.Margin = new Padding(10, 10, 10, 50);
            tabControlStudent.Name = "tabControlStudent";
            tabControlStudent.SelectedIndex = 0;
            tabControlStudent.Size = new Size(358, 440);
            tabControlStudent.TabIndex = 0;
            // 
            // tabPersonal
            // 
            tabPersonal.Controls.Add(dtpDateOfBirth);
            tabPersonal.Controls.Add(txtPhone);
            tabPersonal.Controls.Add(txtEmail);
            tabPersonal.Controls.Add(txtLastName);
            tabPersonal.Controls.Add(txtFirstName);
            tabPersonal.Controls.Add(lblPhone);
            tabPersonal.Controls.Add(lblEmail);
            tabPersonal.Controls.Add(lblDateOfBirth);
            tabPersonal.Controls.Add(lblLastName);
            tabPersonal.Controls.Add(lblFirstName);
            tabPersonal.Controls.Add(txtStudentID);
            tabPersonal.Controls.Add(lblStudentID);
            tabPersonal.Location = new Point(4, 29);
            tabPersonal.Name = "tabPersonal";
            tabPersonal.Padding = new Padding(3);
            tabPersonal.Size = new Size(350, 407);
            tabPersonal.TabIndex = 0;
            tabPersonal.Text = "Personal Info";
            tabPersonal.UseVisualStyleBackColor = true;
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Location = new Point(15, 225);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(200, 27);
            dtpDateOfBirth.TabIndex = 12;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(15, 345);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(200, 27);
            txtPhone.TabIndex = 11;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(15, 285);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 10;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(15, 165);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(200, 27);
            txtLastName.TabIndex = 8;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(15, 105);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(200, 27);
            txtFirstName.TabIndex = 7;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(15, 320);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(108, 20);
            lblPhone.TabIndex = 6;
            lblPhone.Text = "Phone Number";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(15, 260);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(103, 20);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email Address";
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Location = new Point(15, 200);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(94, 20);
            lblDateOfBirth.TabIndex = 4;
            lblDateOfBirth.Text = "Date of Birth";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(15, 140);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(79, 20);
            lblLastName.TabIndex = 3;
            lblLastName.Text = "Last Name";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(15, 80);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(83, 20);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "First Name:";
            // 
            // txtStudentID
            // 
            txtStudentID.Location = new Point(15, 45);
            txtStudentID.Name = "txtStudentID";
            txtStudentID.PlaceholderText = "FOE.41.008.140.22";
            txtStudentID.Size = new Size(200, 27);
            txtStudentID.TabIndex = 1;
            // 
            // lblStudentID
            // 
            lblStudentID.AutoSize = true;
            lblStudentID.Location = new Point(15, 20);
            lblStudentID.Name = "lblStudentID";
            lblStudentID.Size = new Size(79, 20);
            lblStudentID.TabIndex = 0;
            lblStudentID.Text = "Student ID";
            // 
            // tabAcademic
            // 
            tabAcademic.Controls.Add(dtpEnrollmentDate);
            tabAcademic.Controls.Add(nudCWA);
            tabAcademic.Controls.Add(cmbDepartment);
            tabAcademic.Controls.Add(cmbCourse);
            tabAcademic.Controls.Add(cmbYearLevel);
            tabAcademic.Controls.Add(cmbFaculty);
            tabAcademic.Controls.Add(lblEnrolmentDate);
            tabAcademic.Controls.Add(lblCWA);
            tabAcademic.Controls.Add(lblYearLevel);
            tabAcademic.Controls.Add(lblCourse);
            tabAcademic.Controls.Add(lblDepartment);
            tabAcademic.Controls.Add(lblFaculty);
            tabAcademic.Location = new Point(4, 29);
            tabAcademic.Name = "tabAcademic";
            tabAcademic.Padding = new Padding(3);
            tabAcademic.Size = new Size(350, 407);
            tabAcademic.TabIndex = 1;
            tabAcademic.Text = "Academic Info";
            tabAcademic.UseVisualStyleBackColor = true;
            // 
            // dtpEnrollmentDate
            // 
            dtpEnrollmentDate.Location = new Point(15, 345);
            dtpEnrollmentDate.Name = "dtpEnrollmentDate";
            dtpEnrollmentDate.Size = new Size(200, 27);
            dtpEnrollmentDate.TabIndex = 11;
            // 
            // nudCWA
            // 
            nudCWA.DecimalPlaces = 2;
            nudCWA.Location = new Point(15, 285);
            nudCWA.Name = "nudCWA";
            nudCWA.Size = new Size(200, 27);
            nudCWA.TabIndex = 10;
            // 
            // cmbDepartment
            // 
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Items.AddRange(new object[] { "Computer Science and Engineering", "Mechanical Engineering", "Electrical and Electronics Engineering", "Mathematics", "Environmental and Safety Engineering" });
            cmbDepartment.Location = new Point(15, 105);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(200, 28);
            cmbDepartment.TabIndex = 9;
            // 
            // cmbCourse
            // 
            cmbCourse.FormattingEnabled = true;
            cmbCourse.Items.AddRange(new object[] { "Comuter Science and Engineering", "CyberSecurity", "Information Systems" });
            cmbCourse.Location = new Point(15, 165);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(200, 28);
            cmbCourse.TabIndex = 8;
            // 
            // cmbYearLevel
            // 
            cmbYearLevel.FormattingEnabled = true;
            cmbYearLevel.Items.AddRange(new object[] { "100", "200", "300", "400" });
            cmbYearLevel.Location = new Point(15, 225);
            cmbYearLevel.Name = "cmbYearLevel";
            cmbYearLevel.Size = new Size(200, 28);
            cmbYearLevel.TabIndex = 7;
            // 
            // cmbFaculty
            // 
            cmbFaculty.FormattingEnabled = true;
            cmbFaculty.Items.AddRange(new object[] { "Engineering", "Science", "Arts", "Business", "Medicine" });
            cmbFaculty.Location = new Point(15, 45);
            cmbFaculty.Name = "cmbFaculty";
            cmbFaculty.Size = new Size(200, 28);
            cmbFaculty.TabIndex = 6;
            // 
            // lblEnrolmentDate
            // 
            lblEnrolmentDate.AutoSize = true;
            lblEnrolmentDate.Location = new Point(15, 320);
            lblEnrolmentDate.Name = "lblEnrolmentDate";
            lblEnrolmentDate.Size = new Size(116, 20);
            lblEnrolmentDate.TabIndex = 5;
            lblEnrolmentDate.Text = "Enrolment Date:";
            // 
            // lblCWA
            // 
            lblCWA.AutoSize = true;
            lblCWA.Location = new Point(15, 260);
            lblCWA.Name = "lblCWA";
            lblCWA.Size = new Size(259, 20);
            lblCWA.TabIndex = 4;
            lblCWA.Text = "CWA (Cumulative Weighted Average):";
            // 
            // lblYearLevel
            // 
            lblYearLevel.AutoSize = true;
            lblYearLevel.Location = new Point(15, 200);
            lblYearLevel.Name = "lblYearLevel";
            lblYearLevel.Size = new Size(78, 20);
            lblYearLevel.TabIndex = 3;
            lblYearLevel.Text = "Year Level:";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(15, 140);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(57, 20);
            lblCourse.TabIndex = 2;
            lblCourse.Text = "Course:";
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(15, 80);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(92, 20);
            lblDepartment.TabIndex = 1;
            lblDepartment.Text = "Department:";
            // 
            // lblFaculty
            // 
            lblFaculty.AutoSize = true;
            lblFaculty.Location = new Point(15, 20);
            lblFaculty.Name = "lblFaculty";
            lblFaculty.Size = new Size(57, 20);
            lblFaculty.TabIndex = 0;
            lblFaculty.Text = "Faculty:";
            // 
            // tabSummary
            // 
            tabSummary.Controls.Add(rtbSummary);
            tabSummary.Location = new Point(4, 29);
            tabSummary.Name = "tabSummary";
            tabSummary.Padding = new Padding(3);
            tabSummary.Size = new Size(350, 407);
            tabSummary.TabIndex = 2;
            tabSummary.Text = "Summary";
            tabSummary.UseVisualStyleBackColor = true;
            // 
            // rtbSummary
            // 
            rtbSummary.BorderStyle = BorderStyle.None;
            rtbSummary.Dock = DockStyle.Fill;
            rtbSummary.Location = new Point(3, 3);
            rtbSummary.Name = "rtbSummary";
            rtbSummary.ReadOnly = true;
            rtbSummary.Size = new Size(344, 401);
            rtbSummary.TabIndex = 0;
            rtbSummary.Text = "";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(151, 20);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(151, 20);
            toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(splitContainer1);
            Controls.Add(txtSearch);
            Controls.Add(btnAddStudent);
            Controls.Add(panel1);
            MinimumSize = new Size(800, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StuDash";
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            groupBoxDetails.ResumeLayout(false);
            tabControlStudent.ResumeLayout(false);
            tabPersonal.ResumeLayout(false);
            tabPersonal.PerformLayout();
            tabAcademic.ResumeLayout(false);
            tabAcademic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCWA).EndInit();
            tabSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnAddStudent;
        private Button btnDeleteStudent;
        private Button btnEditStudent;
        private TextBox txtSearch;
        private SplitContainer splitContainer1;
        private DataGridView dgvStudents;
        private GroupBox groupBoxDetails;
        private TabControl tabControlStudent;
        private TabPage tabPersonal;
        private TabPage tabAcademic;
        private TabPage tabSummary;
        private TextBox txtStudentID;
        private Label lblStudentID;
        private Label lblPhone;
        private Label lblEmail;
        private Label lblDateOfBirth;
        private Label lblLastName;
        private Label lblFirstName;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private DateTimePicker dtpDateOfBirth;
        private Label lblEnrolmentDate;
        private Label lblCWA;
        private Label lblYearLevel;
        private Label lblCourse;
        private Label lblDepartment;
        private Label lblFaculty;
        private DateTimePicker dtpEnrollmentDate;
        private NumericUpDown nudCWA;
        private ComboBox cmbDepartment;
        private ComboBox cmbCourse;
        private ComboBox cmbYearLevel;
        private ComboBox cmbFaculty;
        private RichTextBox rtbSummary;
        private Button btnClearForm;
        private Button btnSaveStudent;
        private Button btnCancel;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ErrorProvider errorProvider;
    }
}

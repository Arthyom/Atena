using System;
using System.Collections.Generic;
using System.Linq;
using Atena.Helpers;
using Atena.Models;
using Atena.Services.Interfaces;
using DataBaseContext;

// using DataBaseContext;

// using DataBaseContext;
using Gtk;
using Microsoft.EntityFrameworkCore;
using UI = Gtk.Builder.ObjectAttribute;

namespace Atena
{
    public class MainWindow : Window
    {
        private readonly CaadiDbContext _dbContext;
        private readonly IAtenaReport _report;

        private readonly AtenaGlobalConfigs _configs;

        [UI] private Label _label1 = null;

        // [UI] private Button _button1 = null;

        [UI] private Box _box1 = null;

        [UI] private ComboBoxText _comboBox_Periods = null;
        [UI] private ComboBoxText _comboBox_Groups = null;

        private string selectedPeriodId, selectedGroupId, selectedPeriodAsText;

        private ListStore _listPeriods = new ListStore(typeof(int), typeof(string));

        private List<Student> studentsByGroupd = new List<Student>();

        private Dictionary<string, List<Visit>> visitsByStudent = new Dictionary<string, List<Visit>>();
        private Dictionary<string, List<IGrouping<int, Visit>>> visitsByStudentGrouped = new Dictionary<string, List<IGrouping<int, Visit>>>();

        private Dictionary<string, string> visitByStudentComputed = new Dictionary<string, string>();





        private int _counter;

        public MainWindow(CaadiDbContext dbContext, IAtenaReport report, AtenaGlobalConfigs configs) : this(new Builder("MainWindow.glade"))
        {
            _dbContext = dbContext;

           _configs = configs;

            _report = report;
        }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            Shown += Window_Shown;
            // _button1.Clicked += Button1_Clicked;
        }
        
        private void Window_Shown(object sender, EventArgs e)
        {
            if( !string.IsNullOrEmpty( _configs.ConnectionString))
            {     
                _dbContext.Database.OpenConnection();
                _dbContext.Periods.ToList().ForEach((p) =>
                {
                    _comboBox_Periods.Append(p.Id.ToString(), p.Description);
                });

                _comboBox_Periods.Changed += ComboBoxText_Periods_Change;
                _comboBox_Groups.Changed += ComboBoxText_Groups_Change;
            }
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {
            var p = _dbContext.Periods.ToList();
            var s = _dbContext.Visits.ToList();

            p.Reverse();
            p.ForEach(x =>
            {
                Button bn = new Button(x.Description);
                _box1.PackStart(bn, true, true, 5);
            });

            _box1.ShowAll();


            _counter++;
            _label1.Text = "Hello World! This button has been clicked " + _counter + " time(s).";
        }


        private void ComboBoxText_Periods_Change(object sender, EventArgs e)
        {
            _comboBox_Groups.RemoveAll();
            var mappedSender = (ComboBoxText)sender;
            selectedPeriodId = mappedSender.ActiveId;
            selectedPeriodAsText = mappedSender.ActiveText;
            _dbContext
                .Groups
                .Where(g => g.Periodid.ToString() == selectedPeriodId)
                .ToList()
                .ForEach(g =>
                {
                    string teacherName = _dbContext.Teachers
                    .Where(t => t.Employeenumber == g.Employeenumber)
                    .Select(t => $"{t.Name} {t.Firstlastname}")
                    .FirstOrDefault() ?? "No Teacher's Name";

                    string label = $"{teacherName} - {g.Level}{g.Identifier}";
                    _comboBox_Groups.Append(g.Id.ToString(), label);
                });
        }
        
        private void ComboBoxText_Groups_Change(object sender, EventArgs e)
        {   
            studentsByGroupd.Clear();
            visitsByStudent.Clear();
            visitsByStudentGrouped.Clear();
            var mappedSender = (ComboBoxText)sender;
            selectedGroupId = mappedSender.ActiveId;

            studentsByGroupd = _dbContext
            .GroupMembers
            .Where(gm => gm.Groupid.ToString() == selectedGroupId)
            .Select(gm => _dbContext.Students.Where(s => s.Nua == gm.Nua).FirstOrDefault())
            .ToList();

            studentsByGroupd.ForEach(sbg =>
            {
                var relatedVisits = _dbContext
                                        .Visits
                                        .Where(
                                                v => 
                                                v.Nua == sbg.Nua &&
                                                v.Periodid.ToString() == selectedPeriodId
                                            )
                                        .ToList();
                visitsByStudent.Add(sbg.Nua, relatedVisits);
            });

            visitsByStudent.Keys.ToList().ForEach(k =>
            {
                visitsByStudentGrouped.Add(k, visitsByStudent[k].GroupBy(vs => vs.Start.Value.Month).ToList());
            });

            visitByStudentComputed = AtenaHelpersVisits.countAndCoumputeVisits(visitsByStudentGrouped);

            var c = AtenaHelpersVisits.CorrectVistsFromComputed(visitByStudentComputed);

            var s  = AtenaHelpersHours.switchNuaByMonthAsKey(c);

            var docs = _report
            .getArraysToPdsFromData(selectedPeriodAsText, mappedSender.ActiveText, s);

            _report.savePdfs(selectedPeriodAsText,mappedSender.ActiveText, docs);
            

        
        }
    }
}

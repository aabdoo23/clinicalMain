﻿using clinical.BaseClasses;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for treatmentPlanPage.xaml
    /// </summary>

    internal class dataObj
    {
        internal string Notes { get; set; }
        internal string Freq { get; set; }
        internal string Exercise { get; set; }
        internal int ExerciseId { get; set; }
        internal dataObj() { }
        internal dataObj(string notes, string freq, string exercise)
        {
            Notes = notes;
            Freq = freq;
            Exercise = exercise;
        }
    }


    public partial class treatmentPlanPage : Page
    {
        Patient currentPatient;
        Visit currentVisit;

        int planID;

        List<dataObj> ObjectsList = new List<dataObj>();

        List<string> commonFreq = new List<string> { "2 times a day", "3 times a week", "4 times a life" };

        public treatmentPlanPage(Visit currVisit, Patient currPatient) //new
        {
            InitializeComponent();
            planID = globals.generateNewPrescriptionID(currVisit.VisitID, DateTime.Now);
            currentPatient = currPatient;
            currentVisit = currVisit;
            todayDatePicker.SelectedDate = DateTime.Now;
            patientNameTextBox.Text = currentPatient.FirstName + " " + currentPatient.LastName;
            patientAgeTextBox.Text = currentPatient.Age.ToString();
            mainStackPanel.Children.Clear();
            injuriesCB.ItemsSource = DB.GetAllInjuries();
            injuriesCB.SelectedIndex = 0;
            planNameTextBox.ItemsSource = DB.GetAllTreatmentPlans();
        }
        public treatmentPlanPage(TreatmentPlan treatmentPlan) //view and update
        {
            InitializeComponent();
            planID = treatmentPlan.PlanID;
            currentVisit = DB.GetVisitByID(treatmentPlan.VisitID);
            currentPatient = DB.GetPatientById(treatmentPlan.PatientID);
            todayDatePicker.SelectedDate = DateTime.Now;
            patientNameTextBox.Text = currentPatient.FirstName + " " + currentPatient.LastName;
            patientAgeTextBox.Text = currentPatient.Age.ToString();
            List<Injury> injuries = DB.GetAllInjuries();
            injuriesCB.ItemsSource = injuries;

            planTimeInWeeksTextBox.Text = treatmentPlan.PlanTimeInWeeks.ToString();
            planNameTextBox.ItemsSource = DB.GetAllTreatmentPlans();
            planNameTextBox.SelectedItem = treatmentPlan.PlanName;
            mainStackPanel.Children.Clear();
            List<IssueExercise> IssuedExercises = DB.GetIssuedExercisesByTreatmentPlanID(treatmentPlan.PlanID);
            foreach (IssueExercise issue in IssuedExercises)
            {
                CreateNewIssueExercise(issue);
            }
        }
        private void CreateNewIssueExercise()
        {


            Border border = new Border
            {
                Height = 100,
                Margin = new Thickness(5),
                CornerRadius = new CornerRadius(5),
                BorderBrush = (Brush)FindResource("lightFontColor"),
                BorderThickness = new Thickness(5),
                Background = (Brush)FindResource("lighterColor")
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            PackIconMaterial packIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.MinusBox,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)FindResource("lightFontColor"),
                Margin = new Thickness(8, 0, 0, 0),
                Width = 25,
                Height = 25,
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock textBlockExercise = new TextBlock
            {
                Text = "Exercise",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockExercise, 0);
            Grid.SetColumn(textBlockExercise, 0);

            TextBlock textBlockFrequency = new TextBlock
            {
                Text = "Frequency",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };
            Grid.SetRow(textBlockFrequency, 0);
            Grid.SetColumn(textBlockFrequency, 1);

            TextBlock textBlockNotes = new TextBlock
            {
                Text = "Notes",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockNotes, 0);
            Grid.SetColumn(textBlockNotes, 2);

            TextBox notesTextBox = new TextBox
            {
                Name = "notesTXT",
                Margin = new Thickness(8)
            };
            Grid.SetRow(notesTextBox, 1);
            Grid.SetColumn(notesTextBox, 2);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                ItemsSource = DB.GetAllExercises(),
                FilterMode = AutoCompleteFilterMode.Contains

            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);

            AutoCompleteBox frequencyAutoCompleteBox = new AutoCompleteBox
            {
                Name = "frequencyACT",
                Margin = new Thickness(8),
                ItemsSource = commonFreq,
                FilterMode = AutoCompleteFilterMode.Contains
            };
            Grid.SetRow(frequencyAutoCompleteBox, 1);
            Grid.SetColumn(frequencyAutoCompleteBox, 1);

            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockFrequency);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);
            grid.Children.Add(frequencyAutoCompleteBox);
            grid.Children.Add(packIcon);


            border.Child = grid;

            dataObj obj = new dataObj();
            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, obj);
            exerciseAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(exerciseAutoCompleteBox, obj);
            frequencyAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(frequencyAutoCompleteBox, obj);
            packIcon.MouseDown += (sender, e) => removeLast(obj);

            ObjectsList.Add(obj);


            mainStackPanel.Children.Add(border);
        }
        private void CreateNewIssueExercise(IssueExercise issue)
        {


            Border border = new Border
            {
                Height = 100,
                Margin = new Thickness(5),
                CornerRadius = new CornerRadius(5),
                BorderBrush = (Brush)FindResource("lightFontColor"),
                BorderThickness = new Thickness(5),
                Background = (Brush)FindResource("lighterColor")
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            PackIconMaterial packIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.MinusBox,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)FindResource("lightFontColor"),
                Margin = new Thickness(8, 0, 0, 0),
                Width = 25,
                Height = 25,
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock textBlockExercise = new TextBlock
            {
                Text = "Exercise",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockExercise, 0);
            Grid.SetColumn(textBlockExercise, 0);

            TextBlock textBlockFrequency = new TextBlock
            {
                Text = "Frequency",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };
            Grid.SetRow(textBlockFrequency, 0);
            Grid.SetColumn(textBlockFrequency, 1);

            TextBlock textBlockNotes = new TextBlock
            {
                Text = "Notes",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockNotes, 0);
            Grid.SetColumn(textBlockNotes, 2);

            TextBox notesTextBox = new TextBox
            {
                Name = "notesTXT",
                Margin = new Thickness(8),
                Text = issue.Notes
            };
            Grid.SetRow(notesTextBox, 1);
            Grid.SetColumn(notesTextBox, 2);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                ItemsSource = DB.GetAllExercises(),
                FilterMode = AutoCompleteFilterMode.Contains,
                Text = DB.GetExerciseById(issue.ExerciseID).ExerciseName

            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);

            AutoCompleteBox frequencyAutoCompleteBox = new AutoCompleteBox
            {
                Name = "frequencyACT",
                Margin = new Thickness(8),
                ItemsSource = commonFreq,
                FilterMode = AutoCompleteFilterMode.Contains,
                Text = issue.Frequency
            };
            Grid.SetRow(frequencyAutoCompleteBox, 1);
            Grid.SetColumn(frequencyAutoCompleteBox, 1);

            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockFrequency);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);
            grid.Children.Add(frequencyAutoCompleteBox);
            grid.Children.Add(packIcon);

            border.Child = grid;

            dataObj obj = new dataObj();
            obj.Exercise = exerciseAutoCompleteBox.Text;
            obj.ExerciseId = issue.ExerciseID;
            obj.Freq = frequencyAutoCompleteBox.Text;
            obj.Notes = issue.Notes;
            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, obj);
            exerciseAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(exerciseAutoCompleteBox, obj);
            frequencyAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(frequencyAutoCompleteBox, obj);
            packIcon.MouseDown += (sender, e) => removeLast(obj);

            ObjectsList.Add(obj);


            mainStackPanel.Children.Add(border);
        }
        private void CreateNewIssueExercise(dataObj elObj)
        {


            Border border = new Border
            {
                Height = 100,
                Margin = new Thickness(5),
                CornerRadius = new CornerRadius(5),
                BorderBrush = (Brush)FindResource("lightFontColor"),
                BorderThickness = new Thickness(5),
                Background = (Brush)FindResource("lighterColor")
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            PackIconMaterial packIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.MinusBox,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)FindResource("lightFontColor"),
                Margin = new Thickness(8, 0, 0, 0),
                Width = 25,
                Height = 25,
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock textBlockExercise = new TextBlock
            {
                Text = "Exercise",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockExercise, 0);
            Grid.SetColumn(textBlockExercise, 0);

            TextBlock textBlockFrequency = new TextBlock
            {
                Text = "Frequency",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };
            Grid.SetRow(textBlockFrequency, 0);
            Grid.SetColumn(textBlockFrequency, 1);

            TextBlock textBlockNotes = new TextBlock
            {
                Text = "Notes",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockNotes, 0);
            Grid.SetColumn(textBlockNotes, 2);

            TextBox notesTextBox = new TextBox
            {
                Name = "notesTXT",
                Margin = new Thickness(8),
                Text = elObj.Notes
            };
            Grid.SetRow(notesTextBox, 1);
            Grid.SetColumn(notesTextBox, 2);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                ItemsSource = DB.GetAllExercises(),
                FilterMode = AutoCompleteFilterMode.Contains,
                Text = elObj.Exercise

            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);

            AutoCompleteBox frequencyAutoCompleteBox = new AutoCompleteBox
            {
                Name = "frequencyACT",
                Margin = new Thickness(8),
                ItemsSource = commonFreq,
                FilterMode = AutoCompleteFilterMode.Contains,
                Text = elObj.Freq
            };
            Grid.SetRow(frequencyAutoCompleteBox, 1);
            Grid.SetColumn(frequencyAutoCompleteBox, 1);

            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockFrequency);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);
            grid.Children.Add(frequencyAutoCompleteBox);
            grid.Children.Add(packIcon);

            border.Child = grid;


            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, elObj);
            exerciseAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(exerciseAutoCompleteBox, elObj);
            frequencyAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(frequencyAutoCompleteBox, elObj);
            packIcon.MouseDown += (sender, e) => removeLast(elObj);


            mainStackPanel.Children.Add(border);
        }


        private void UpdateDataObject(TextBox textBox, dataObj dataObject)
        {
            if (textBox.Name == "notesTXT")
            {
                dataObject.Notes = textBox.Text;
            }
        }
        private void UpdateDataObject(AutoCompleteBox textBox, dataObj dataObject)
        {
            if (textBox.Name == "exerciseACT")
            {
                dataObject.Exercise = textBox.Text;
            }
            else if (textBox.Name == "frequencyACT")
            {
                dataObject.Freq = textBox.Text;

            }
        }

        private void print(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Please make sure to save before printing.");
            globals.PrintPage(new PrintingPage(DB.GetTreatmentPlanByID(planID)));
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            bool pending = false;
            if (ObjectsList.Count > 0)
            {
                int cnt = 0;
                foreach (dataObj obj in ObjectsList)
                {
                    bool found = false;

                    foreach (Exercise ex in DB.GetAllExercises())
                    {
                        if (obj.Exercise == null || obj.Exercise.Length == 0) break;
                        if (ex.ExerciseName.Trim().ToLower().Equals(obj.Exercise.Trim().ToLower()) || ex.ExerciseName.Trim().ToLower().Contains(obj.Exercise.Trim().ToLower()))
                        {
                            found = true;
                            obj.ExerciseId = ex.ExerciseID;
                            break;
                        }
                    }
                    if (!found)
                    {
                        MessageBoxResult result = MessageBox.Show(obj.Exercise + " in cell " + (cnt + 1).ToString() + " was not found, do you want to add it to the Exercises Library?", "New Exercise Detected",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            Exercise ex = new Exercise(new Random().Next(999), obj.Exercise, " ", " ");
                            obj.ExerciseId = ex.ExerciseID;
                            DB.InsertExercise(ex);
                        }
                        else if (result == MessageBoxResult.No)
                        {
                            MessageBox.Show("Please refer to the exercise in cell " + (cnt + 1).ToString() + " and edit it or pick an exercise from the recommendations to be able to proceed.");
                            pending = true;
                            break;
                        }
                    }
                    cnt++;
                }
            }
            else
            {
                MessageBox.Show("Can't save an empty treatment Plan.");
                pending = true;
            }

            if (!pending)
            {
                string planName = planNameTextBox.Text;

                TreatmentPlan tp = DB.GetTreatmentPlanByID(planID); //check if a plan with the same id is there (opened the form with a plan)



                foreach (TreatmentPlan t in DB.GetAllTreatmentPlans()) //double check if they wrote the name of an existing plan (time complexity not good)
                {
                    if (t.PlanName.Trim().ToLower().Equals(planName.Trim().ToLower()) || t.PlanName.Trim().ToLower().Contains(planName.Trim().ToLower()))
                    {
                        tp = t;
                        break;
                    }

                }

                if (injuriesCB.SelectedItem == null) { MessageBox.Show("Please select an injury related to this treatment plan first before you can save."); return; }

                int injuryId = ((Injury)injuriesCB.SelectedItem).InjuryID;
                int planTimeInWeeks = int.Parse(planTimeInWeeksTextBox.Text);
                int visId = (currentVisit == null) ? 0 : currentVisit.VisitID;
                int physId = (currentVisit == null) ? 0 : currentVisit.PhysiotherapistID;
                int patId = (currentPatient == null) ? 0 : currentPatient.PatientID;
                string keyw = "";
                if (currentPatient != null)
                {
                    foreach (Injury i in DB.GetAllInjuriesByPatientID(currentPatient.PatientID))
                    {
                        keyw += $"{i.InjuryName},";
                    }
                }
                if (tp != null) //update plan, leaving name as is
                {
                    tp.InjuryID = injuryId;
                    tp.PlanTimeInWeeks = planTimeInWeeks;

                    tp.Keywords = keyw;
                    tp.Timestamp = DateTime.Now;
                    DB.UpdateTreatmentPlan(tp);
                    MessageBox.Show("Treatment Plan Updated, ID: " + planID.ToString());
                    List<IssueExercise> planIssuedExes = DB.GetIssuedExercisesByTreatmentPlanID(tp.PlanID); //updating issues (not good but gets job done?)
                    foreach (var i in planIssuedExes)
                    {
                        DB.DeleteIssueExercise(i.IssueID);
                    }
                    foreach (dataObj obj in ObjectsList)
                    {
                        IssueExercise issue = new IssueExercise(globals.generateNewIssueExerciseID(planID, currentPatient.PatientID), obj.ExerciseId, currentPatient.PatientID, planID, obj.Freq, obj.Notes);
                        DB.InsertIssueExercise(issue);
                    }
                    return;

                }
                //else
                tp = new TreatmentPlan(planID, planName, planTimeInWeeks, 0, "", keyw, injuryId, patId, visId, DateTime.Now);
                DB.InsertTreatmentPlan(tp);
                foreach (dataObj obj in ObjectsList)
                {
                    IssueExercise issue = new IssueExercise(globals.generateNewIssueExerciseID(planID, currentPatient.PatientID), obj.ExerciseId, currentPatient.PatientID, planID, obj.Freq, obj.Notes);
                    DB.InsertIssueExercise(issue);
                }
                MessageBox.Show("Treatment plan Saved, ID: " + planID.ToString());

            }
        }


        private void injuriesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //planNameTextBox.Text = ((Injury)injuriesCB.SelectedItem).InjuryName + " Plan";
        }


        private void closeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }

        private void minimizeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void newExcersice(object sender, MouseButtonEventArgs e)
        {


            CreateNewIssueExercise();

        }

        private void newTreatment(object sender, MouseButtonEventArgs e)
        {

        }

        private void removeLast(dataObj obj)
        {
            ObjectsList.Remove(obj);
            mainStackPanel.Children.Clear();
            foreach (dataObj dataObj in ObjectsList)
            {
                CreateNewIssueExercise(dataObj);
            }
        }


        ///printing
        /////////////////////////////////////////////////
        ///

        private void planNameACSelectedPlanChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBoxResult res = MessageBox.Show("Are you sure you want to discard all current modifications and retrieve all data from this plan?", "Confirmation", MessageBoxButton.YesNo);
            //if (res == MessageBoxResult.No) { planNameTextBox.SelectedItem = (TreatmentPlan)DB.GetTreatmentPlanByID(planID); return; }

            string planName = planNameTextBox.Text;

            TreatmentPlan selectedPlan = null;
            foreach (TreatmentPlan t in DB.GetAllTreatmentPlans()) //double check if they wrote the name of an existing plan (time complexity not good)
            {
                if (t.PlanName.Trim().ToLower().Equals(planName.Trim().ToLower()) || t.PlanName.Trim().ToLower().Contains(planName.Trim().ToLower()))
                {
                    selectedPlan = t;
                    break;
                }

            }
            if (selectedPlan == null) return;
            planID = selectedPlan.PlanID;


            int cnt = 0;
            foreach (var i in DB.GetAllInjuries())
            {
                if (i.InjuryID == selectedPlan.InjuryID)
                {
                    injuriesCB.SelectedIndex = cnt;
                    break;
                }
                cnt++;
            }
            planTimeInWeeksTextBox.Text = selectedPlan.PlanTimeInWeeks.ToString();

            mainStackPanel.Children.Clear();
            List<IssueExercise> IssuedExercises = DB.GetIssuedExercisesByTreatmentPlanID(selectedPlan.PlanID);
            foreach (IssueExercise issue in IssuedExercises)
            {
                CreateNewIssueExercise(issue);
            }
        }
    }
}

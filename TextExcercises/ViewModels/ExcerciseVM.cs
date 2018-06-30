using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextExcercises.Entities;
using TextExcercises.Enums;

namespace TextExcercises.ViewModels
{
    class ExcerciseVM: BindableBase
    {
        private List<ExcerciseEntity> _excercises;
        private List<string> _excerciseNames;
        private ExcerciseEntity _selectedExcercise;

        public List<ExcerciseEntity> Excercises
        {
            get { return _excercises; }
            set { _excercises = value; }
        }

        public List<string> ExcerciseNames
        {
            get { return _excerciseNames; }
            set { _excerciseNames = value; }
        }

        public ExcerciseEntity SelectedExcercise
        {
            get { return _selectedExcercise; }
            set 
            { 
                _selectedExcercise = value;
                RaisePropertyChanged("SelectedExcercise");
            }
        }

        public ExcerciseVM()
        {
            XDocument xmlQuestionDocument = XDocument.Load(Path.Combine(Environment.CurrentDirectory, @"Content\Excercises.xml"));
            Excercises = new List<ExcerciseEntity>();
            ExcerciseNames = new List<string>();
            var excercises = from a in xmlQuestionDocument.Elements("Excercises")
                            select a;
            foreach (var item in excercises.Elements())
            {
                ExcerciseEntity excercise = new ExcerciseEntity
                {
                    Name = item.Element("Name").Value,
                    Content = item.Element("Content").Value,
                    Type = (ExcerciseTypes)Enum.Parse(typeof(ExcerciseTypes), item.Element("Type").Value)
                };
                Excercises.Add(excercise);
                ExcerciseNames.Add(excercise.Name);
            }
        }

        public DelegateCommand ProcessCommand
        {
            get
            {
                return new DelegateCommand(ProcessData);
            }
        }

        private void ProcessData()
        {
            if (SelectedExcercise.Type == ExcerciseTypes.Reverse)
            {
                SelectedExcercise.Output = ReverseString(SelectedExcercise.Input);
            }
        }

        private string ReverseString(string input)
        {
            string output = "";
            input = string.IsNullOrEmpty(input) ? "" : input;
            for (int i = input.Length-1; i >=0; i--)
            {
                output += input[i];
            }
                return output;
        }
    }
}

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
                return new DelegateCommand(ProcessData, CanProcessData).ObservesProperty(() => SelectedExcercise); ;
            }
        }

        private bool CanProcessData()
        {
            return SelectedExcercise != null;
        }

        private void ProcessData()
        {
            string input = string.IsNullOrEmpty(SelectedExcercise.Input) ? "" : SelectedExcercise.Input;
            switch (SelectedExcercise.Type)
            {
                case ExcerciseTypes.Reverse:
                    SelectedExcercise.Output = ReverseString(input);
                    break;
                case ExcerciseTypes.PigLatin:
                    break;
                case ExcerciseTypes.Vowels:
                    SelectedExcercise.Output = CountVowels(input).ToString();
                    break;
                case ExcerciseTypes.Palindrome:
                    SelectedExcercise.Output = CheckIfPalindrome(input).ToString();
                    break;
            }
        }

        private string ReverseString(string input)
        {
            string output = "";
            for (int i = input.Length-1; i >=0; i--)
            {
                output += input[i];
            }
                return output;
        }

        private readonly List<char> _vowels = new List<char> { 'a', 'e', 'o', 'u', 'i', 'y' };

        private int CountVowels(string input)
        {
            int output = 0;
            foreach (char c in input)
            {
                if (_vowels.Contains(c))
                {
                    output++;
                }
            }
            return output;
        }

        private bool CheckIfPalindrome(string input)
        {
            for (int i = 0; i<input.Length; i++)
            {
                if (input[i]!=input[input.Length-1-i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

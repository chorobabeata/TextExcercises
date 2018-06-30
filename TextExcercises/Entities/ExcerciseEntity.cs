using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextExcercises.Enums;

namespace TextExcercises.Entities
{
    class ExcerciseEntity : BindableBase
    {
        private ExcerciseTypes _type;
        private string _name;
        private string _content;
        private string _input;
        private string _output;

        public ExcerciseTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                RaisePropertyChanged("Name");
            }
        }        

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged("Content");
            }
        }

        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                RaisePropertyChanged("Input");
            }
        }        

        public string Output
        {
            get { return _output; }
            set
            {
                _output = value;
                RaisePropertyChanged("Output");
            }
        }
    }
}

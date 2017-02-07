using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RationBuilder.Models;

namespace RationBuilder.ViewModels
{
    public class HumanCharacteristicViewModel : BaseViewModel
    {
        private HumanCharacteristic humanCharacteristic = new HumanCharacteristic();
        private ObservableCollection<string> weightItems = new ObservableCollection<string>();
        private ObservableCollection<string> heightItems = new ObservableCollection<string>();
        private ObservableCollection<string> dailyActivityItems = new ObservableCollection<string>();
        private ObservableCollection<string> purposeActivityItems = new ObservableCollection<string>();
        private ObservableCollection<string> genderItems = new ObservableCollection<string>();
        private string selectedWeight = null;
        private string selectedHeight = null;
        private string selectedDailyActivity = null;
        private string selectedPurposeActivity = null;
        private string selectedGender = null;

        public ObservableCollection<string> WeightItems
        { get { return weightItems; } }
        public ObservableCollection<string> HeightItems
        { get { return heightItems; } }
        public ObservableCollection<string> DailyActivityItems
        { get { return dailyActivityItems; } }
        public ObservableCollection<string> PurposeActivityItems
        { get { return purposeActivityItems; } }
        public ObservableCollection<string> GenderItems
        { get { return genderItems; } }
        public int Weight { get { return humanCharacteristic.ParseWeight(selectedWeight); } }
        public int Height { get { return humanCharacteristic.ParseHeight(selectedHeight); } }
        public DailyActivity DailyActivity { get { return humanCharacteristic.ParseDailyActivity(selectedDailyActivity); } }
        public PurposeActivity PurposeActivity { get { return humanCharacteristic.ParsePurposeActivity(selectedPurposeActivity); } }
        public Gender Gender { get { return humanCharacteristic.ParseGender(selectedGender); } }
        public string SelectedWeight
        {
            get { return selectedWeight; }
            set { SetValue<string>(ref selectedWeight, value); }
        }
        public string SelectedHeight
        {
            get { return selectedHeight; }
            set { SetValue<string>(ref selectedHeight, value); }
        }
        public string SelectedDailyActivity
        {
            get { return selectedDailyActivity; }
            set { SetValue<string>(ref selectedDailyActivity, value); }
        }
        public string SelectedPurposeActivity
        {
            get { return selectedPurposeActivity; }
            set { SetValue<string>(ref selectedPurposeActivity, value); }
        }
        public string SelectedGender
        {
            get { return selectedGender; }
            set { SetValue<string>(ref selectedGender, value); }
        }
        public HumanCharacteristicViewModel()
        {
            Action<List<string>, ObservableCollection<string>> copy = (list, obs) =>
                {
                    foreach (string elem in list)
                    {
                        obs.Add(elem);
                    }
                };
            copy(humanCharacteristic.Weight, weightItems);
            copy(humanCharacteristic.Height, heightItems);
            copy(humanCharacteristic.DailyActivity, dailyActivityItems);
            copy(humanCharacteristic.PurposeActivity, purposeActivityItems);
            copy(humanCharacteristic.Gender, genderItems);
            SelectedWeight = weightItems[0];
            SelectedHeight = heightItems[0];
            SelectedPurposeActivity = purposeActivityItems[0];
            SelectedDailyActivity = dailyActivityItems[0];
            SelectedGender = genderItems[0];
        }
        public HumanCharacteristicViewModel(IUser user) :this()
        {
            if (humanCharacteristic.GetWeight(user.Weight) == null)
            {
                SelectedWeight = WeightItems[0];
            }
            else
            {
                SelectedWeight = humanCharacteristic.GetWeight(user.Weight);
            }
                    
            if (humanCharacteristic.GetHeight(user.Height) == null)
            {
                SelectedHeight = HeightItems[0];
            }
            else
            {
                SelectedHeight = humanCharacteristic.GetHeight(user.Height);
            }
            SelectedPurposeActivity = humanCharacteristic.GetPurposeActivity(user.PurposeActivity);
            SelectedDailyActivity = humanCharacteristic.GetDailyActivity(user.DailyActivity);
            SelectedGender = humanCharacteristic.GetGender(user.Gender);
        }
    }
}

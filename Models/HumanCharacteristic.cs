using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class HumanCharacteristic
    {
        private List<string> weight = new List<string>();
        private List<string> height = new List<string>();
        private List<string> dailyActivity = new List<string>();
        private List<string> purposeActivity = new List<string>();
        private List<string> gender = new List<string>();

        public List<string> Weight
        { get { return weight; } }
        public List<string> Height
        { get { return height; } }
        public List<string> DailyActivity
        { get { return dailyActivity; } }
        public List<string> PurposeActivity
        { get { return purposeActivity; } }
        public List<string> Gender
        { get { return gender; } }
        public HumanCharacteristic()
        {
            height.Add(L10n.Resource.HEIGHT_NONE);
            weight.Add(L10n.Resource.WEIGHT_NONE);
            for (int i = 140; i < 241; i++)
            {
                height.Add(i.ToString());
            }
            for (int i = 40; i < 130; i++)
            {
                weight.Add(i.ToString());
            }
            dailyActivity.Add(L10n.Resource.DAILY_ACTIVITY_NONE);
            dailyActivity.Add(L10n.Resource.DAILY_ACTIVITY_SIT);
            dailyActivity.Add(L10n.Resource.DAILY_ACTIVITY_LIGHT);
            dailyActivity.Add(L10n.Resource.DAILY_ACTIVITY_MEDIUM);
            dailyActivity.Add(L10n.Resource.DAILY_ACTIVITY_HIGH);
            dailyActivity.Add(L10n.Resource.DAILY_ACTIVITY_EXTREME);
            purposeActivity.Add(L10n.Resource.PURPOSE_ACTIVITY_NONE);
            purposeActivity.Add(L10n.Resource.PURPOSE_ACTIVITY_UP_WEIGHT);
            purposeActivity.Add(L10n.Resource.PURPOSE_ACTIVITY_DOWN_WEIGHT);
            gender.Add(L10n.Resource.GENDER_MALE);
            gender.Add(L10n.Resource.GENDER_FEMALE);
        }
        public string GetWeight(int weight)
        {
            return this.weight.FirstOrDefault(i => i == weight.ToString());
        }
        public string GetHeight(int height)
        {
            return this.height.FirstOrDefault(i => i == height.ToString());
        }
        public string GetDailyActivity(DailyActivity dailyActivity)
        {
            string dA = null;
            switch(dailyActivity)
            {
                case Models.DailyActivity.None:
                    dA = this.dailyActivity[0];
                    break;
                case Models.DailyActivity.NoActivity:
                    dA = this.dailyActivity[1];
                    break;
                case Models.DailyActivity.LightActivity:
                    dA = this.dailyActivity[2];
                    break;
                case Models.DailyActivity.MediumActivity:
                    dA = this.dailyActivity[3];
                    break;
                case Models.DailyActivity.HighActivity:
                    dA = this.dailyActivity[4];
                    break;
                case Models.DailyActivity.ExtremeActivity:
                    dA = this.dailyActivity[5];
                    break;
            }
            return dA;
        }
        public string GetPurposeActivity(PurposeActivity purposeActivity)
        {
            string pA = null;
            switch (purposeActivity)
            {
                case Models.PurposeActivity.None:
                    pA = this.purposeActivity[0];
                    break;
                case Models.PurposeActivity.UpWeight:
                    pA = this.purposeActivity[1];
                    break;
                case Models.PurposeActivity.DownWeight:
                    pA = this.purposeActivity[2];
                    break;
            }
            return pA;
        }
        public string GetGender(Gender gender)
        {
            string g = null;
            switch (gender)
            {
                case Models.Gender.Male:
                    g = this.gender[0];
                    break;
                case Models.Gender.Female:
                    g = this.gender[1];
                    break;
            }
            return g;
        }
        public int ParseWeight(string item)
        {
            int parsed = weight.FindIndex(i => i == item);
            if(parsed != -1)
            {
                parsed = Convert.ToInt32(weight[parsed]);
            }
            return parsed;
        }
        public int ParseHeight(string item)
        {
            int parsed = height.FindIndex(i => i == item);
            if (parsed != -1)
            {
                parsed = Convert.ToInt32(height[parsed]);
            }
            return parsed;
        }
        public DailyActivity ParseDailyActivity(string item)
        {
            DailyActivity activity = Models.DailyActivity.None;
            int objectIndex = dailyActivity.FindIndex(i => i == item);
            if (objectIndex > 0)
            {
                switch(objectIndex)
                {
                    case 1:
                        activity = Models.DailyActivity.NoActivity;
                        break;
                    case 2:
                        activity = Models.DailyActivity.LightActivity;
                        break;
                    case 3:
                        activity = Models.DailyActivity.MediumActivity;
                        break;
                    case 4:
                        activity = Models.DailyActivity.HighActivity;
                        break;
                    case 5:
                        activity = Models.DailyActivity.ExtremeActivity;
                        break;
                }
            }
            return activity;
        }
        public PurposeActivity ParsePurposeActivity(string item)
        {
            PurposeActivity activity = Models.PurposeActivity.None;
            int objectIndex = purposeActivity.FindIndex(i => i == item);
            if (objectIndex > 0)
            {
                switch (objectIndex)
                {
                    case 1:
                        activity = Models.PurposeActivity.UpWeight;
                        break;
                    case 2:
                        activity = Models.PurposeActivity.DownWeight;
                        break;
                }
            }
            return activity;
        }
        public Gender ParseGender(string item)
        {
            Gender ourGender = Models.Gender.Male;
            int objectIndex = gender.FindIndex(i => i == item);
            if (objectIndex > 0)
            {
                switch (objectIndex)
                {
                    case 1:
                        ourGender = Models.Gender.Male;
                        break;
                    case 2:
                        ourGender = Models.Gender.Female;
                        break;
                }
            }
            return ourGender;
        }
    }
}

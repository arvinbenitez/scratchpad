using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AguaDeMaria.Model;

namespace AguaDeMaria.Common.Data
{
    public class SettingsManager
    {
        private IRepository<Setting> _settingRepository;

        public SettingsManager(IRepository<Setting> settingRepository)
        {
            this.SettingRepository = settingRepository;
        }

        private IRepository<Setting> SettingRepository
        {
            get { return _settingRepository; }
            set { _settingRepository = value; }
        }

        public string GetNextOrderNumber()
        {
            var setting = GetSetting();
            var newNumber = setting.OrderNumberCounter++;
            this.SettingRepository.Update(setting);
            return newNumber.ToString("0000000000");
        }

        private Setting GetSetting()
        {
            var setting = this.SettingRepository.Get(x => x.SettingId > 0).FirstOrDefault();
            if (setting == null)
            {
                setting = GenerateDefaultSetting();
            }
            return setting;
        }

        private Setting GenerateDefaultSetting()
        {
            var setting = new Setting() {DeliveryReceiptNumberCounter = 1, OrderNumberCounter = 1};
            this.SettingRepository.Insert(setting);
            this.SettingRepository.Commit();
            return setting;
        }

        public string GetNextDeliveryReceiptNumber()
        {
            var setting = GetSetting();
            var newNumber = setting.DeliveryReceiptNumberCounter++;
            this.SettingRepository.Update(setting);
            return newNumber.ToString("0000000000");
        }
    }
}

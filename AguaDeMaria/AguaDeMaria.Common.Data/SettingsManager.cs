using System.Linq;
using AguaDeMaria.Model;

namespace AguaDeMaria.Common.Data
{
    public class SettingsManager
    {
        private IRepository<Setting> _settingRepository;

        public SettingsManager(IRepository<Setting> settingRepository)
        {
            SettingRepository = settingRepository;
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
            SettingRepository.Update(setting);
            SettingRepository.Commit();
            return newNumber.ToString("0000000000");
        }

        private Setting GetSetting()
        {
            var setting = SettingRepository.Get(x => x.SettingId > 0).FirstOrDefault();
            if (setting == null)
            {
                setting = GenerateDefaultSetting();
            }
            return setting;
        }

        private Setting GenerateDefaultSetting()
        {
            var setting = new Setting {DeliveryReceiptNumberCounter = 1, OrderNumberCounter = 1};
            SettingRepository.Insert(setting);
            SettingRepository.Commit();
            return setting;
        }

        public string GetNextDeliveryReceiptNumber()
        {
            var setting = GetSetting();
            var newNumber = setting.DeliveryReceiptNumberCounter++;
            SettingRepository.Update(setting);
            SettingRepository.Commit();
            return newNumber.ToString("0000000000");
        }

        public string GetNextPickupSlipNumber()
        {
            var setting = GetSetting();
            var newNumber = setting.PickupSlipNumberCounter++;
            SettingRepository.Update(setting);
            SettingRepository.Commit();
            return newNumber.ToString("0000000000");

        }

        public string GetNextInvoiceNumber()
        {
            var setting = GetSetting();
            var newNumber = setting.InvoiceNumberCounter++;
            SettingRepository.Update(setting);
            SettingRepository.Commit();
            return newNumber.ToString("0000000000");
        }
    }
}

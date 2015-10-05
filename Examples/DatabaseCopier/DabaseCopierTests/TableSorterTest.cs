using System.Collections.Generic;
using System.Linq;
using DatabaseCopier.Library;
using DatabaseCopier.Library.Database;
using DatabaseCopier.Library.Helper;
using NUnit.Framework;

namespace DabaseCopierTests
{
    [TestFixture]
    public class TableSorterTest
    {
        private TableSorter sorter;

        [SetUp]
        public void Init()
        {
            sorter = new TableSorter();
        }

        [Test]
        public void When_Table_Sources_No_Dependencies_Should_Sort_By_Name()
        {
            var list = new List<TableSource>
            {
                new TableSource {Name = "Node"},
                new TableSource {Name = "Account"},
                new TableSource {Name = "Location"}
            };

            var sortedList = sorter.Sort(list);

            Assert.That(sortedList, Has.Count.EqualTo(3));
            Assert.That(sortedList[0].Name,Is.EqualTo("Account"));
            Assert.That(sortedList[1].Name,Is.EqualTo("Location"));
            Assert.That(sortedList[2].Name,Is.EqualTo("Node"));
        }        
        
        [Test]
        public void When_Table_Has_Dependencies_Should_Sort_By_Dependency()
        {
            var list = new List<TableSource>
            {
                new TableSource {Name = "Node"},
                new TableSource {Name = "AccountApplication", TableDependencies = new[] {"Account","Application"}},
                new TableSource {Name = "Application"},
                new TableSource {Name = "Account", TableDependencies = new[] {"Application"}}
            };

            var sortedList = sorter.Sort(list);

            Assert.That(sortedList, Has.Count.EqualTo(4));
            Assert.That(sortedList[0].Name,Is.EqualTo("Account"));
            Assert.That(sortedList[1].Name, Is.EqualTo("Application"));
            Assert.That(sortedList[2].Name, Is.EqualTo("AccountApplication"));
            Assert.That(sortedList[3].Name, Is.EqualTo("Node"));
        }

        [Test]
        public void When_Table_Has_Layers_Of_Dependencies_Should_Sort_By_Dependency()
        {
            var list = new List<TableSource>();
            //list.Add(TableSource.ToTableSource("{ Name: 'RoleType',TableDependencies:['RoleTypeGroup']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ContentType',TableDependencies:['ContentType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DaylightSaving',TableDependencies:['TimeZone']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'VideoMessage',TableDependencies:['AccountApplication', 'ApplicationSession', 'Message', 'Node', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ActionType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Location',TableDependencies:['Location']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DBVersion',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppSetting',TableDependencies:['AccountApplication', 'Setting']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'VideoMessageFormat',TableDependencies:['TranscodingFormat', 'VideoMessage']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'HelpTopic',TableDependencies:['HelpTopic']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Language',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DestinationType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryNote',TableDependencies:['AccountApplication', 'NodeDestination', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TaxCode',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Action',TableDependencies:['Action', 'ActionType', 'HelpTopic']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AuthenticationType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryProviderStatus',TableDependencies:['DeliveryProviderType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppTaxCode',TableDependencies:['AccountApplication', 'TaxCode']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Login',TableDependencies:['ApplicationSession', 'AuthenticationType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DestinationNote',TableDependencies:['Destination', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'EventTypeGroup',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RoleTypeAction',TableDependencies:['Action', 'RoleType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCConditionAction',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'EventType',TableDependencies:['EventTypeGroup', 'Workflow']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'LoginSession',TableDependencies:['Login', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DelegateType',TableDependencies:['CustomField']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCConditionOperation',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppWorkflowEventType',TableDependencies:['AccountApplication', 'EventType', 'Workflow']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DestinationQcCondition',TableDependencies:['Destination', 'QCAttribute', 'QCConditionAction', 'QCConditionOperation']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDelegation',TableDependencies:['ApplicationSession', 'DelegateType', 'Node', 'RoleType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'Application',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApiConsumerApiEndpoint',TableDependencies:['ApiConsumer', 'ApiEndpoint']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Group',TableDependencies:['AccountApplication', 'ApplicationSession', 'Group']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryProviderApprovalReason',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DestinationRemoteServer',TableDependencies:['Destination', 'RemoteServer']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Currency',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DestinationSupportGroup',TableDependencies:['Destination']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'PhysicalServer',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'GroupType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'PaymentTerm',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DisallowedDestination',TableDependencies:['Destination', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryProviderStatusReason',TableDependencies:['DeliveryProviderApprovalReason', 'DeliveryProvider', 'DeliveryProviderStatus']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RemoteServerType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'StorageProvider',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'EventWorkflowHistory',TableDependencies:['Event', 'Workflow']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'GroupGroupType',TableDependencies:['Group', 'GroupType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApprovalParticipant',TableDependencies:['AccountApplication', 'GroupType', 'RoleType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FileDownload',TableDependencies:['File', 'StorageProvider', 'AccountApplication', 'ApplicationSession', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'LayoutType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FileStorageProviderHistory',TableDependencies:['StorageProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RemoteServer',TableDependencies:['ApprovalParticipant', 'DeliveryXmlFormat', 'PhysicalServer', 'RemoteServerType', 'TranscodingFormat', 'User', 'Workflow']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingPackageSetting',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Layout',TableDependencies:['ApplicationSession', 'AccountApplication', 'LayoutType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Company',TableDependencies:['ApplicationSession', 'Company', 'User', 'Location', 'Location', 'Location', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDeliveryProviderHistoryReason',TableDependencies:['DeliveryProviderApprovalReason', 'NodeDeliveryProviderHistory']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FileStorageProvider',TableDependencies:['ApplicationSession', 'File', 'StorageProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingFormatSourcePackage',TableDependencies:['MediaFormat', 'TranscodingFormat', 'TranscodingPackage', 'TranscodingPackageSetting']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'SubsidaryMetaMediaFormat',TableDependencies:['MetaMediaFormat', 'Subsidary']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDestinationRemoteServer',TableDependencies:['ApplicationSession', 'ApprovalStatus', 'NodeDestination', 'NodeDestinationStatus', 'RemoteServer', 'StorageProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Subsidary',TableDependencies:['Company', 'Currency', 'PaymentTerm', 'StorageProvider', 'SubsidiaryRegion']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'EventStatus',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'LayoutCustomField',TableDependencies:['ApplicationSession', 'CustomField', 'Layout']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Account',TableDependencies:['ApplicationSession', 'Company', 'Subsidary']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FilterType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Event',TableDependencies:['AccountApplication', 'ApplicationSession', 'EventStatus', 'EventType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'AccountApplication',TableDependencies:['AccountApplication', 'Application', 'ApplicationSession', 'User', 'Account']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'SubsidaryMetaMediaFormatMetaDeliveryType',TableDependencies:['MetaDeliveryType', 'SubsidaryMetaMediaFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Filter',TableDependencies:['AccountApplication', 'ApplicationSession', 'FilterType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FilterContentType',TableDependencies:['ContentType', 'Filter']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TextType',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'ApplicationSession',TableDependencies:['AccountApplication', 'LoginSession']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FilterItem',TableDependencies:['CustomField', 'Filter']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Text',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MetadataRuleGroup',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'GroupUser',TableDependencies:['ApplicationSession', 'Group', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RuleContext',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'GrabSendOrder',TableDependencies:['AccountApplication', 'Node', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TextTextType',TableDependencies:['Text', 'TextType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MetadataRuleType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Role',TableDependencies:['AccountApplication', 'ApplicationSession', 'Role', 'RoleType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'User',TableDependencies:['ApplicationSession', 'Language', 'Location', 'Location', 'Location', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeAttachmentContextFile',TableDependencies:['AttachmentContext', 'File', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Report',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CensorApprovalStatus',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MetadataRule',TableDependencies:['MetadataRuleType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCInfoType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RoleAction',TableDependencies:['Action', 'ApplicationSession', 'Role']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'GroupReport',TableDependencies:['Group', 'Report']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MetadataRuleCode',TableDependencies:['Brand', 'Company', 'CustomField', 'MetadataRule', 'Product']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeStatus',TableDependencies:['ApplicationSession']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MessageTemplateType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MessageType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TextTranslation',TableDependencies:['AccountApplication', 'Language', 'Text']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Node',TableDependencies:['AccountApplication', 'ApplicationSession', 'AccountApplication', 'ContentType', 'AccountApplication', 'User', 'DeliveryOrderStatus', 'Node', 'AccountApplication', 'User', 'NodeStatus', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MessageTemplate',TableDependencies:['ApplicationSession', 'Language', 'AccountApplication', 'MessageTemplateType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Message',TableDependencies:['AccountApplication', 'ApplicationSession', 'Language', 'Message', 'MessageType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCAttributeType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCAttribute',TableDependencies:['CustomField', 'QCAttributeType', 'QCInfoType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeGroupRole',TableDependencies:['ApplicationSession', 'Group', 'Node', 'Node', 'Role']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApprovalStatus',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'IngestRequest',TableDependencies:['ApplicationSession', 'Group', 'Message', 'Node', 'User', 'AccountApplication']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MetadataRuleGroupRule',TableDependencies:['MetadataRule', 'MetadataRuleGroup']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'View',TableDependencies:['AccountApplication', 'LayoutType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDestinationStatus',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'IngestRequestXml',TableDependencies:['IngestRequest', 'IngestXmlType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FileVersion',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'WorkflowType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MessageBin',TableDependencies:['Bin', 'Message']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppMetadataRuleGroup',TableDependencies:['AccountApplication', 'MetadataRuleGroup']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'File',TableDependencies:['AccountApplication', 'ApplicationSession', 'File', 'File', 'FileType', 'FileVersion', 'QCFormat', 'TranscodingFormat', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Workflow',TableDependencies:['WorkflowType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MessageFile',TableDependencies:['File', 'Message']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeLayout',TableDependencies:['AccountApplication', 'ApplicationSession', 'ContentType', 'Group', 'Layout', 'Node', 'Node', 'View', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CensorshipProcess',TableDependencies:['Workflow']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'TranscodingPackage',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MessageRecipientType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppMetadataRuleSettings',TableDependencies:['AccountApplication', 'AccountApplication', 'MetadataRuleGroupRule', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Deployment_Version',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeStorageProviderSetting',TableDependencies:['AccountApplication', 'Node', 'Node', 'StorageProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Destination',TableDependencies:['CensorshipProcess', 'Company', 'Destination', 'Location', 'Location', 'Location', 'Workflow']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MessageRecipient',TableDependencies:['AccountApplication', 'Group', 'Message', 'MessageRecipientType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingPackageMediaFormat',TableDependencies:['MediaFormat', 'TranscodingPackage']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDestinationRemoteServerFile',TableDependencies:['File', 'NodeDestinationRemoteServer']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingFormatContext',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'SubsidiaryRegion',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeCensorStatus',TableDependencies:['Censor', 'CensorApprovalStatus', 'Classification', 'Node', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeFile',TableDependencies:['ApplicationSession', 'File', 'Node']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'CommandType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingPackageTranscodingFormat',TableDependencies:['TranscodingFormat', 'TranscodingPackage']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'TranscodingFormatRule',TableDependencies:['TranscodingFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeCustomFieldDefaultValue',TableDependencies:['AccountApplication', 'ContentType', 'CustomField', 'CustomFieldDefaultValueType', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApprovalTracking',TableDependencies:['AccountApplication', 'ApprovalParticipant', 'ApprovalStatus', 'ApprovalTracking', 'NodeDestination', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Rule',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'Command',TableDependencies:['CommandType', 'TranscodingFormatRule', 'TranscodingPackage']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDeliveryProviderStatus',TableDependencies:['DeliveryProvider', 'DeliveryProviderStatus', 'Node', 'Workflow']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingPackageMediaFormatTranscodingFormat',TableDependencies:['TranscodingPackageMediaFormat', 'TranscodingPackageTranscodingFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDestinationSetting',TableDependencies:['ApplicationSession', 'Destination', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppNote',TableDependencies:['AccountApplication', 'User']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'TranscodingFormat',TableDependencies:['Command', 'MetaMediaFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppRule',TableDependencies:['AccountApplication', 'RuleContext', 'User', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MetaDeliveryType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeNote',TableDependencies:['Node', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountManagementRole',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeNotification',TableDependencies:['Node', 'NodeNotificationEventType', 'NodeNotificationRecipientType', 'NodeNotificationType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountUserRole',TableDependencies:['Account', 'AccountManagementRole', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryType',TableDependencies:['AccountApplication', 'DeliveryType', 'Destination', 'MetaDeliveryType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeTranscodingFormat',TableDependencies:['AccountApplication', 'Node', 'TranscodingFormat', 'TranscodingFormatContext']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AddressType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeNotificationMessage',TableDependencies:['Message', 'Node', 'NodeNotification']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryTypeComplexFee',TableDependencies:['DeliveryType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Address',TableDependencies:['AddressType', 'Company', 'Location', 'Location', 'Location', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDestination',TableDependencies:['AccountApplication', 'AccountApplication', 'ApplicationSession', 'ApprovalStatus', 'DeliveryProviderStatus', 'DeliveryProviderStatus', 'User', 'User', 'DeliveryProviderStatus', 'DeliveryType', 'Destination', 'Node', 'NodeDestinationStatus', 'NodeDestination']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeSetting',TableDependencies:['Node', 'AccountApplication', 'Setting']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DestinationTemplate',TableDependencies:['AccountApplication', 'User', 'User', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AttachmentContext',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TeamNotification',TableDependencies:['AccountAppRule', 'User', 'User', 'NodeNotificationEventType', 'Team']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'FileType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApplicationAction',TableDependencies:['Action', 'Application']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApiEntityHistory',TableDependencies:['AccountApplication', 'User', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'StorageProviderEndpoint',TableDependencies:['StorageProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeTransferMapping',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ContentTypeFileTypeRule',TableDependencies:['AccountApplication', 'ContentType', 'FileType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'BinType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Channel',TableDependencies:['Destination']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppAttachmentContext',TableDependencies:['AccountApplication', 'AttachmentContext']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Bin',TableDependencies:['AccountApplication', 'ApplicationSession', 'BinType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppBillingGroup',TableDependencies:['AccountApplication', 'Group']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDestinationChannel',TableDependencies:['ApplicationSession', 'Channel', 'NodeDestination']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Page',TableDependencies:['AccountApplication', 'ApplicationSession', 'Layout']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDestinationTemplate',TableDependencies:['AccountApplication', 'DestinationTemplate', 'Node', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'InvoiceFrequency',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'MediaFormat',TableDependencies:['MetaMediaFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'PageContentType',TableDependencies:['ApplicationSession', 'ContentType', 'Page']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Setting',TableDependencies:['SettingType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppBillingSetting',TableDependencies:['AccountApplication', 'Currency', 'InvoiceFrequency', 'PaymentTerm']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Brand',TableDependencies:['ApplicationSession', 'Company']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCAttributeValueFile',TableDependencies:['File', 'QCAttribute']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppAttachmentContextFileType',TableDependencies:['AccountAppAttachmentContext', 'FileType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'KeyNumberAlgorithm',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'BinNode',TableDependencies:['Bin', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeNotificationType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryProviderType',TableDependencies:['Workflow']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Product',TableDependencies:['ApplicationSession', 'Brand']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCFormatAttributeValue',TableDependencies:['QCAttribute', 'QCFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Censor',TableDependencies:['AccountApplication', 'CensorshipProcess', 'RoleType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApiEndpoint',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryProvider',TableDependencies:['AccountApplication', 'DeliveryProviderType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCCondition',TableDependencies:['QCConditionAction', 'QCConditionOperation', 'QCFormatAttributeValue']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'MetaMediaFormat',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'KeyNumberAlgorithmCode',TableDependencies:['Brand', 'Company', 'CustomField', 'KeyNumberAlgorithm', 'Product']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountApiMapping',TableDependencies:['AccountApplication']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApiConsumer',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppDeliveryProvider',TableDependencies:['AccountApplication', 'DeliveryProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RemoteServerMessageStatus',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryAttachmentContextFile',TableDependencies:['AttachmentContext', 'File', 'NodeDestination']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldContext',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Team',TableDependencies:['User', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ApiEndpointAction',TableDependencies:['ApiEndpoint']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Classification',TableDependencies:['Censor']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppDeliveryProviderSetting',TableDependencies:['AccountApplication', 'DelegateType', 'DeliveryProvider', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RemoteServerMessageType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldCustomFieldContext',TableDependencies:['CustomField', 'CustomFieldContext']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserRemoteServer',TableDependencies:['RemoteServer', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CensorApprovalHistory',TableDependencies:['ApprovalParticipant', 'Censor', 'CensorApprovalStatus', 'Classification', 'Node', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryXmlFormat',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RemoteServerMessage',TableDependencies:['RemoteServerMessageType', 'RemoteServer', 'RemoteServerMessageStatus']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TeamUser',TableDependencies:['Team', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ChannelRemoteServer',TableDependencies:['Channel', 'RemoteServer']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppDeliveryXmlFormat',TableDependencies:['AccountApplication', 'DeliveryXmlFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldLookup',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RemoteServerStorageProvider',TableDependencies:['RemoteServer', 'StorageProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDeliveryProviderHistory',TableDependencies:['DeliveryProvider', 'DeliveryProviderStatus', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CompanyType',TableDependencies:['ApplicationSession']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppDestinationSetting',TableDependencies:['AccountApplication', 'Destination']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Aggregate',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RemoteServerTranscodingFormat',TableDependencies:['RemoteServer', 'TranscodingFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TeamNotificationMessage',TableDependencies:['AccountApplication', 'Message', 'Node', 'NodeNotificationEventType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CompanyCompanyType',TableDependencies:['Company', 'CompanyType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountApplicationContentType',TableDependencies:['AccountApplication', 'ContentType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Operator',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ReportTemplate',TableDependencies:['Layout']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'IngestXmlType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldDefaultValue',TableDependencies:['Aggregate', 'CustomField', 'Operator']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ReportType',TableDependencies:['ReportType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDelegationMessage',TableDependencies:['Message', 'NodeDelegation']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CompanyUser',TableDependencies:['ApplicationSession', 'Company', 'User', 'UserType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'OrderXmlSchemaSetting',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountApplicationIngestXmlType',TableDependencies:['AccountApplication', 'IngestXmlType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomField',TableDependencies:['AccountApplication', 'ApplicationSession', 'AttachmentContext', 'CustomField', 'CustomFieldDefaultValue', 'CustomFieldLookup', 'CustomFieldType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'SMSLog',TableDependencies:['Message', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountApplicationMessageTemplateType',TableDependencies:['AccountApplication', 'MessageTemplateType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ComputeInstance',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountApplicationStorageProvider',TableDependencies:['AccountApplication', 'StorageProvider']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'SubsidiaryDeliveryType',TableDependencies:['MetaDeliveryType', 'Subsidary', 'Subsidary']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'AccountApplicationTranscodingFormat',TableDependencies:['AccountApplication', 'TranscodingFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ContactType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppMediaFormat',TableDependencies:['AccountApplication', 'MediaFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingFormatCondition',TableDependencies:['QCAttribute', 'QCConditionAction', 'QCConditionOperation', 'TranscodingFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeDelegationToken',TableDependencies:['AccountApplication', 'AccountApplication', 'ApplicationSession', 'ApplicationSession', 'DelegateType', 'Node', 'User', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Contact',TableDependencies:['ApplicationSession', 'Company', 'ContactType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppNodeDelegation',TableDependencies:['AccountApplication', 'DelegateType', 'RoleType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DisallowedDestinationException',TableDependencies:['Destination', 'Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldOption',TableDependencies:['ApplicationSession', 'CustomField']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TranscodingFormatReplicationSetting',TableDependencies:['AccountApplication', 'Node', 'TranscodingFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ContentTypeAction',TableDependencies:['Action', 'ContentType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeNotificationEventType',TableDependencies:['MessageTemplateType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ContentTypeChild',TableDependencies:['ContentType', 'ContentType']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserAccountApplication',TableDependencies:['AccountApplication', 'ApplicationSession', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'ContentTypeTranscodingFormat',TableDependencies:['ContentType', 'TranscodingFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeCustomField',TableDependencies:['CustomFieldOption', 'MediaFormat', 'ApplicationSession', 'Brand', 'Company', 'CustomField', 'Node', 'Product', 'QCFormat', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserDestinationSetting',TableDependencies:['AccountApplication', 'ApplicationSession', 'Destination', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeNotificationRecipientType',TableDependencies:['CustomField']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'Contract',TableDependencies:['Account', 'User', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'NodeKeywords',TableDependencies:['Node']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserNodeDelegation',TableDependencies:['AccountApplication', 'ApplicationSession', 'DelegateType', 'RoleType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppNodeNotificationSetting',TableDependencies:['AccountApplication', 'NodeNotificationEventType', 'NodeNotificationRecipientType', 'NodeNotificationType', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserNodeNotificationSetting',TableDependencies:['AccountApplication', 'NodeNotificationEventType', 'NodeNotificationRecipientType', 'NodeNotificationType', 'User', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryKeywords',TableDependencies:['NodeDestination']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DestinationGroup',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CT_Settings',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'QCFormat',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserSetting',TableDependencies:['AccountApplication', 'Setting', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldDefaultValueType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppQCFormat',TableDependencies:['AccountApplication', 'FileType', 'MediaFormat', 'QCFormat']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'SubsidaryCompanySettings',TableDependencies:['Company', 'Subsidary']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserStorageProvider',TableDependencies:['AccountApplication', 'StorageProvider', 'User']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'CustomFieldOperation',TableDependencies:['CustomField', 'CustomField', 'Operator']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'RoleTypeGroup',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'DeliveryOrderStatus',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'AccountAppDestinationGroup',TableDependencies:['AccountApplication', 'DestinationGroup']}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'TimeZone',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'SettingType',TableDependencies:[]}"));
            //list.Add(TableSource.ToTableSource("{ Name: 'UserUserType',TableDependencies:['ApplicationSession', 'User', 'UserType']}"));

            var sortedList = sorter.Sort(list);

            //Assert.That(sortedList.IndexOf("ApplicationSession"), Is.LessThan(sortedList.IndexOf("Bin")),"ApplicationSession vs Bin");
            //Assert.That(sortedList.IndexOf("DestinationGroup"), Is.LessThan(sortedList.IndexOf("AccountAppDestinationGroup")), "DestinationGroup vs AccountAppDestinationGroup");
            Assert.That(sortedList.IndexOf("TranscodingFormat"), Is.LessThan(sortedList.IndexOf("AccountApplicationTranscodingFormat")), "TranscodingFormat vs AccountApplicationTranscodingFormat");

        }
    }

    internal static class TestExtensions
    {
        public static int IndexOf(this IList<TableSource> tables, string tableName)
        {
            var table = tables.FirstOrDefault(x => x.Name == tableName);
            return table == null ? -1 : tables.IndexOf(table);
        }
    }
}

        /// <summary>
        /// Gets all message definitions in a certain directory, necessary to be able to choose the applicable definition on screen
        /// </summary>
        /// <returns></returns>
        private static List<MessageDefinition> GetMessageDefinitions()
        {
            var messagedefinitions = new List<MessageDefinition>();

            var directory = StateManager.Settings["XmlMessageDefinition.Export$Attribute.Name.Pascal$"].ToString();
            var files = Directory.GetFiles(directory, "*.xml");

            foreach (var file in files)
            {
                var definition = XDocument.Load(file);
                var message = definition.Element("message");

                if (message == null) continue;

                messagedefinitions.Add(new MessageDefinition { Name = message.GetAttributeOrDefault("name"), FileName = file });
            }

            return messagedefinitions;
        }

        public void Export$Attribute.Name.Pascal$(string messageDefinitionType)
        {
            var messageDefinitionFileName = MessageDefinitions.First(md => md.Name == messageDefinitionType).FileName;

            try
            {
                ExportFile = $Attribute.Name.Pascal$Factory.ExportToFile($Attribute.Name.Pascal$, messageDefinitionFileName);
            }
            catch (MessagingException ex)
            {
                LogManager.Log(ex);
                ValidationManager.AddError(ex.Message);
            }
        }


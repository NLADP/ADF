            if (p.Length == 0)
            {
                this.RunTask(ApplicationTasks.Select$Attribute.Type.Pascal$);
				return;
            }

			Master = ($Attribute.Type.Pascal$) p[0];
                
            GetDetails();

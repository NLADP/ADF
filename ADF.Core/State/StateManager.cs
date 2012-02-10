using Adf.Core.Objects;

namespace Adf.Core.State
{
    /// <summary>
    /// Represents the states used in the framework.
    /// Provides properties to get the states viz. Application, Personal and Settings.
    /// </summary>
	public static class StateManager
	{
        // cache state providers for performance improvement
        private static IStateProvider application;
        private static readonly object _appLock = new object();

        /// <summary>
        /// Gets the application state.
        /// </summary>
        /// <returns>
        /// The application state.
        /// </returns>
    	public static IStateProvider Application
    	{
            get { lock (_appLock) return application ?? (application = ObjectFactory.BuildUp<IStateProvider>("Application")); }
    	}

        private static IStateProvider personal;
        private static readonly object _personalLock = new object();

        /// <summary>
        /// Gets the personal state.
        /// </summary>
        /// <returns>
        /// The personal state.
        /// </returns>
    	public static IStateProvider Personal
    	{
            get { lock(_personalLock) return personal ?? (personal = ObjectFactory.BuildUp<IStateProvider>("Personal")); }
    	}

        private static IStateProvider settings;
        private static readonly object _setLock = new object();

        /// <summary>
        /// Gets the settings state.
        /// </summary>
        /// <returns>
        /// The settings state.
        /// </returns>
    	public static IStateProvider Settings
    	{
            get { lock(_setLock) return settings ?? (settings = ObjectFactory.BuildUp<IStateProvider>("Settings")); }
    	}
	}
}

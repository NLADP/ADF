using System.Collections;
using Adf.Core.Domain;

namespace Adf.Data.InternalState
{
    /// <summary>
    /// Represents <see cref="System.Collections.Hashtable"/> containg all the rules regarding InternalStatus.
    /// Provides methods to add rule, change internal status etc.
    /// </summary>
    public class InternalStatusEngine
    {
        private readonly Hashtable rules = new Hashtable();

        /// <summary>
        /// Initilizes a new instance of <see cref="InternalStatusEngine"/> class.
        /// </summary>
        public InternalStatusEngine()
        {
            BuildUp();    
        }

        /// <summary>
        /// Adds all the rules regarding <see cref="InternalStatus"/> to the <see cref="InternalStatusEngine"/>.
        /// </summary>
        public void BuildUp()
        {
            AddRule(InternalStatus.Changed, InternalStatus.Ok);
            AddRule(InternalStatus.Changed, InternalStatus.Removed);

            AddRule(InternalStatus.Ok, InternalStatus.Changed);
            AddRule(InternalStatus.Ok, InternalStatus.Removed);

            AddRule(InternalStatus.New, InternalStatus.Changed, InternalStatus.NewChanged);
            AddRule(InternalStatus.New, InternalStatus.Removed, InternalStatus.NewRemoved);
            AddRule(InternalStatus.New, InternalStatus.Ok);
            AddRule(InternalStatus.New, InternalStatus.NewChanged);
            AddRule(InternalStatus.New, InternalStatus.NewRemoved);

            AddRule(InternalStatus.NewChanged, InternalStatus.Ok);
            AddRule(InternalStatus.NewChanged, InternalStatus.Removed, InternalStatus.NewRemoved);

            AddRule(InternalStatus.Undefined, InternalStatus.Ok);
            AddRule(InternalStatus.Undefined, InternalStatus.Changed, InternalStatus.NewChanged);
            AddRule(InternalStatus.Undefined, InternalStatus.Removed, InternalStatus.NewRemoved);
            AddRule(InternalStatus.Undefined, InternalStatus.New);
            AddRule(InternalStatus.Undefined, InternalStatus.NewChanged);
            AddRule(InternalStatus.Undefined, InternalStatus.NewRemoved);
        }

        /// <summary>
        /// Returns a string comprising the specified current <see cref="InternalStatus"/> and desired 
        /// <see cref="InternalStatus"/>.
        /// </summary>
        /// <param name="current">The current <see cref="InternalStatus"/>.</param>
        /// <param name="desired">The desired <see cref="InternalStatus"/>.</param>
        /// <returns>
        /// The string comprising the specified current and the desired 
        /// <see cref="InternalStatus"/>es.
        /// </returns>
        private static string ParseKey(InternalStatus current, InternalStatus desired)
        {
            return string.Format("{0} : {1}", current, desired);           
        }

        /// <summary>
        /// Adds a rule regarding the specified current, desired and outcome <see cref="InternalStatus"/>es to 
        /// the <see cref="InternalStatusEngine"/>.
        /// </summary>
        /// <param name="current">The current <see cref="InternalStatus"/>.</param>
        /// <param name="desired">The desired <see cref="InternalStatus"/>.</param>
        /// <param name="outcome">The outcome <see cref="InternalStatus"/>.</param>
        public void AddRule(InternalStatus current, InternalStatus desired, InternalStatus outcome)
        {
            string key = ParseKey(current, desired);
            
            rules[key] = outcome;
        }

        /// <summary>
        /// Adds a rule regarding the specified current and desired <see cref="InternalStatus"/>es to 
        /// the <see cref="InternalStatusEngine"/>.
        /// </summary>
        /// <param name="current">The current <see cref="InternalStatus"/>.</param>
        /// <param name="desired">The desired <see cref="InternalStatus"/>.</param>
        public void AddRule(InternalStatus current, InternalStatus desired)
        {
            AddRule(current, desired, desired);
        }

        /// <summary>
        /// Changes the <see cref="InternalStatus"/> using the specified current and desired 
        /// <see cref="InternalStatus"/>es and returns the resultant <see cref="InternalStatus"/>.
        /// </summary>
        /// <param name="current">The current <see cref="InternalStatus"/>.</param>
        /// <param name="desired">The desired <see cref="InternalStatus"/>.</param>
        /// <returns>
        /// If the generated <see cref="InternalStatus"/> is not null then the generated 
        /// <see cref="InternalStatus"/> is returned; otherwise, the current 
        /// <see cref="InternalStatus"/> is returned.
        /// </returns>
        public InternalStatus ChangeStatus(InternalStatus current, InternalStatus desired)
        {
            string key = ParseKey(current, desired);

            var newstatus = rules[key] as InternalStatus;
            
            return newstatus ?? current;
        }        
    } 
}

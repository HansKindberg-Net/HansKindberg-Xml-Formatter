using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using HansKindberg.Collections;

namespace HansKindberg.Xml.Formatting.Configuration
{
	public class XmlFormatElement : ConfigurationElement, IXmlFormat
	{
		#region Fields

		public const string AttributeNameComparisonPropertyName = "attributeNameComparison";
		public const string AttributeNamesToCorrectCommaSeparatedValuesForPropertyName = "attributeNamesToCorrectCommaSeparatedValuesFor";
		public const string AttributeNamesToPinFirstPropertyName = "attributeNamesToPinFirst";
		public const string AttributeValueComparisonPropertyName = "attributeValueComparison";
		public const string AttributesAlphabeticalSortDirectionPropertyName = "attributesAlphabeticalSortDirection";
		public const string CloseEmptyElementsPropertyName = "closeEmptyElements";
		public const string CommentFormatPropertyName = "commentFormat";
		public const string ElementLevelsToExcludeFromSortingAlphabeticallyPropertyName = "elementLevelsToExcludeFromSortingAlphabetically";
		public const string ElementNameComparisonPropertyName = "elementNameComparison";
		public const string ElementNamesToExcludeChildrenFromSortingAlphabeticallyPropertyName = "elementNamesToExcludeChildrenFromSortingAlphabetically";
		public const string ElementNamesToPinFirstPropertyName = "elementNamesToPinFirst";
		public const string ElementPathsToExcludeChildrenFromSortingAlphabeticallyPropertyName = "elementPathsToExcludeChildrenFromSortingAlphabetically";
		public const string ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyPropertyName = "elementPathsToInvolveChildElementWhenSortingElementsAlphabetically";
		public const string ElementsAlphabeticalSortDirectionPropertyName = "elementsAlphabeticalSortDirection";
		public const string IndentPropertyName = "indent";
		public const string IndentStringPropertyName = "indentString";
		public const string InvolveAttributesWhenSortingElementsAlphabeticallyPropertyName = "involveAttributesWhenSortingElementsAlphabetically";
		public const string MinimumNumberOfAttributesForNewLineOnAttributesPropertyName = "minimumNumberOfAttributesForNewLineOnAttributes";
		public const string NamePropertyName = "name";
		public const string NewLineOnAttributesPropertyName = "newLineOnAttributes";
		public const string NewLineStringPropertyName = "newLineString";
		public const string OmitCommentsPropertyName = "omitComments";
		public const string OmitXmlDeclarationPropertyName = "omitXmlDeclaration";
		public const string SortAttributesAlphabeticallyPropertyName = "sortAttributesAlphabetically";
		public const string SortElementsAlphabeticallyPropertyName = "sortElementsAlphabetically";
		private EventHandledCollection<string> _attributeNamesToCorrectCommaSeparatedValuesFor;
		private EventHandledCollection<string> _attributeNamesToPinFirst;
		private EventHandledCollection<int> _elementLevelsToExcludeFromSortingAlphabetically;
		private EventHandledCollection<string> _elementNamesToExcludeChildrenFromSortingAlphabetically;
		private EventHandledCollection<string> _elementNamesToPinFirst;
		private EventHandledCollection<string> _elementPathsToExcludeChildrenFromSortingAlphabetically;
		private EventHandledCollection<string> _elementPathsToInvolveChildElementWhenSortingElementsAlphabetically;
		private bool _initialized;
		private const string _invalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\";
		private const string _invalidNameCharacters = " ~!@#$%^&*()[]{}/;'\"|\\";
		private const string _invalidPathCharacters = "~!@#$%^&*()[]{};'\"|\\";
		private const string _joinSeparator = ", ";
		private const string _splitSeparator = IntegerCollectionValidator.Separator;

		#endregion

		#region Properties

		[ConfigurationProperty(AttributeNameComparisonPropertyName, DefaultValue = StringComparison.OrdinalIgnoreCase)]
		public virtual StringComparison AttributeNameComparison
		{
			get { return (StringComparison) base[AttributeNameComparisonPropertyName]; }
			set { base[AttributeNameComparisonPropertyName] = value; }
		}

		public virtual ICollection<string> AttributeNamesToCorrectCommaSeparatedValuesFor
		{
			get
			{
				if(this._attributeNamesToCorrectCommaSeparatedValuesFor == null)
				{
					this._attributeNamesToCorrectCommaSeparatedValuesFor = new EventHandledCollection<string>(this.Split(this.AttributeNamesToCorrectCommaSeparatedValuesForInternal));

					this._attributeNamesToCorrectCommaSeparatedValuesFor.Added += (sender, arguments) => this.UpdateAttributeNamesToCorrectCommaSeparatedValuesForInternal(arguments);
					this._attributeNamesToCorrectCommaSeparatedValuesFor.Cleared += (sender, arguments) => this.UpdateAttributeNamesToCorrectCommaSeparatedValuesForInternal(arguments);
					this._attributeNamesToCorrectCommaSeparatedValuesFor.Removed += (sender, arguments) => this.UpdateAttributeNamesToCorrectCommaSeparatedValuesForInternal(arguments);
				}

				return this._attributeNamesToCorrectCommaSeparatedValuesFor;
			}
		}

		[ConfigurationProperty(AttributeNamesToCorrectCommaSeparatedValuesForPropertyName, IsRequired = false), StringValidator(InvalidCharacters = _invalidCharacters, MinLength = 0)]
		protected internal virtual string AttributeNamesToCorrectCommaSeparatedValuesForInternal
		{
			get { return (string) base[AttributeNamesToCorrectCommaSeparatedValuesForPropertyName]; }
			set
			{
				this._attributeNamesToCorrectCommaSeparatedValuesFor = null;
				base[AttributeNamesToCorrectCommaSeparatedValuesForPropertyName] = value;
			}
		}

		public virtual ICollection<string> AttributeNamesToPinFirst
		{
			get
			{
				if(this._attributeNamesToPinFirst == null)
				{
					this._attributeNamesToPinFirst = new EventHandledCollection<string>(this.Split(this.AttributeNamesToPinFirstInternal));

					this._attributeNamesToPinFirst.Added += (sender, arguments) => this.UpdateAttributeNamesToPinFirstInternal(arguments);
					this._attributeNamesToPinFirst.Cleared += (sender, arguments) => this.UpdateAttributeNamesToPinFirstInternal(arguments);
					this._attributeNamesToPinFirst.Removed += (sender, arguments) => this.UpdateAttributeNamesToPinFirstInternal(arguments);
				}

				return this._attributeNamesToPinFirst;
			}
		}

		[ConfigurationProperty(AttributeNamesToPinFirstPropertyName, IsRequired = false), StringValidator(InvalidCharacters = _invalidCharacters, MinLength = 0)]
		protected internal virtual string AttributeNamesToPinFirstInternal
		{
			get { return (string) base[AttributeNamesToPinFirstPropertyName]; }
			set
			{
				this._attributeNamesToPinFirst = null;
				base[AttributeNamesToPinFirstPropertyName] = value;
			}
		}

		[ConfigurationProperty(AttributeValueComparisonPropertyName, DefaultValue = StringComparison.OrdinalIgnoreCase)]
		public virtual StringComparison AttributeValueComparison
		{
			get { return (StringComparison) base[AttributeValueComparisonPropertyName]; }
			set { base[AttributeValueComparisonPropertyName] = value; }
		}

		[ConfigurationProperty(AttributesAlphabeticalSortDirectionPropertyName, DefaultValue = ListSortDirection.Ascending)]
		public virtual ListSortDirection AttributesAlphabeticalSortDirection
		{
			get { return (ListSortDirection) base[AttributesAlphabeticalSortDirectionPropertyName]; }
			set { base[AttributesAlphabeticalSortDirectionPropertyName] = value; }
		}

		[ConfigurationProperty(CloseEmptyElementsPropertyName, DefaultValue = false)]
		public virtual bool CloseEmptyElements
		{
			get { return (bool) base[CloseEmptyElementsPropertyName]; }
			set { base[CloseEmptyElementsPropertyName] = value; }
		}

		[ConfigurationProperty(CommentFormatPropertyName, DefaultValue = XmlCommentFormat.None)]
		public virtual XmlCommentFormat CommentFormat
		{
			get { return (XmlCommentFormat) base[CommentFormatPropertyName]; }
			set { base[CommentFormatPropertyName] = value; }
		}

		public virtual ICollection<int> ElementLevelsToExcludeFromSortingAlphabetically
		{
			get
			{
				if(this._elementLevelsToExcludeFromSortingAlphabetically == null)
				{
					this._elementLevelsToExcludeFromSortingAlphabetically = new EventHandledCollection<int>(this.Split(this.ElementLevelsToExcludeFromSortingAlphabeticallyInternal).Select(int.Parse));

					this._elementLevelsToExcludeFromSortingAlphabetically.Added += (sender, arguments) => this.UpdateElementLevelsToExcludeFromSortingAlphabeticallyInternal(arguments);
					this._elementLevelsToExcludeFromSortingAlphabetically.Cleared += (sender, arguments) => this.UpdateElementLevelsToExcludeFromSortingAlphabeticallyInternal(arguments);
					this._elementLevelsToExcludeFromSortingAlphabetically.Removed += (sender, arguments) => this.UpdateElementLevelsToExcludeFromSortingAlphabeticallyInternal(arguments);
				}

				return this._elementLevelsToExcludeFromSortingAlphabetically;
			}
		}

		[ConfigurationProperty(ElementLevelsToExcludeFromSortingAlphabeticallyPropertyName, IsRequired = false), IntegerCollectionValidator(MinimumLength = 0, MaximumLength = 256)]
		protected internal virtual string ElementLevelsToExcludeFromSortingAlphabeticallyInternal
		{
			get { return (string) base[ElementLevelsToExcludeFromSortingAlphabeticallyPropertyName]; }
			set
			{
				this._elementLevelsToExcludeFromSortingAlphabetically = null;
				base[ElementLevelsToExcludeFromSortingAlphabeticallyPropertyName] = value;
			}
		}

		[ConfigurationProperty(ElementNameComparisonPropertyName, DefaultValue = StringComparison.OrdinalIgnoreCase)]
		public virtual StringComparison ElementNameComparison
		{
			get { return (StringComparison) base[ElementNameComparisonPropertyName]; }
			set { base[ElementNameComparisonPropertyName] = value; }
		}

		public virtual ICollection<string> ElementNamesToExcludeChildrenFromSortingAlphabetically
		{
			get
			{
				if(this._elementNamesToExcludeChildrenFromSortingAlphabetically == null)
				{
					this._elementNamesToExcludeChildrenFromSortingAlphabetically = new EventHandledCollection<string>(this.Split(this.ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal));

					this._elementNamesToExcludeChildrenFromSortingAlphabetically.Added += (sender, arguments) => this.UpdateElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal(arguments);
					this._elementNamesToExcludeChildrenFromSortingAlphabetically.Cleared += (sender, arguments) => this.UpdateElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal(arguments);
					this._elementNamesToExcludeChildrenFromSortingAlphabetically.Removed += (sender, arguments) => this.UpdateElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal(arguments);
				}

				return this._elementNamesToExcludeChildrenFromSortingAlphabetically;
			}
		}

		[ConfigurationProperty(ElementNamesToExcludeChildrenFromSortingAlphabeticallyPropertyName, IsRequired = false), StringValidator(InvalidCharacters = _invalidCharacters, MinLength = 0)]
		protected internal virtual string ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal
		{
			get { return (string) base[ElementNamesToExcludeChildrenFromSortingAlphabeticallyPropertyName]; }
			set
			{
				this._elementNamesToExcludeChildrenFromSortingAlphabetically = null;
				base[ElementNamesToExcludeChildrenFromSortingAlphabeticallyPropertyName] = value;
			}
		}

		public virtual ICollection<string> ElementNamesToPinFirst
		{
			get
			{
				if(this._elementNamesToPinFirst == null)
				{
					this._elementNamesToPinFirst = new EventHandledCollection<string>(this.Split(this.ElementNamesToPinFirstInternal));

					this._elementNamesToPinFirst.Added += (sender, arguments) => this.UpdateElementNamesToPinFirstInternal(arguments);
					this._elementNamesToPinFirst.Cleared += (sender, arguments) => this.UpdateElementNamesToPinFirstInternal(arguments);
					this._elementNamesToPinFirst.Removed += (sender, arguments) => this.UpdateElementNamesToPinFirstInternal(arguments);
				}

				return this._elementNamesToPinFirst;
			}
		}

		[ConfigurationProperty(ElementNamesToPinFirstPropertyName, IsRequired = false), StringValidator(InvalidCharacters = _invalidCharacters, MinLength = 0)]
		protected internal virtual string ElementNamesToPinFirstInternal
		{
			get { return (string) base[ElementNamesToPinFirstPropertyName]; }
			set
			{
				this._elementNamesToPinFirst = null;
				base[ElementNamesToPinFirstPropertyName] = value;
			}
		}

		public virtual ICollection<string> ElementPathsToExcludeChildrenFromSortingAlphabetically
		{
			get
			{
				if(this._elementPathsToExcludeChildrenFromSortingAlphabetically == null)
				{
					this._elementPathsToExcludeChildrenFromSortingAlphabetically = new EventHandledCollection<string>(this.Split(this.ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal));

					this._elementPathsToExcludeChildrenFromSortingAlphabetically.Added += (sender, arguments) => this.UpdateElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal(arguments);
					this._elementPathsToExcludeChildrenFromSortingAlphabetically.Cleared += (sender, arguments) => this.UpdateElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal(arguments);
					this._elementPathsToExcludeChildrenFromSortingAlphabetically.Removed += (sender, arguments) => this.UpdateElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal(arguments);
				}

				return this._elementPathsToExcludeChildrenFromSortingAlphabetically;
			}
		}

		[ConfigurationProperty(ElementPathsToExcludeChildrenFromSortingAlphabeticallyPropertyName, IsRequired = false), StringValidator(InvalidCharacters = _invalidPathCharacters, MinLength = 0)]
		protected internal virtual string ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal
		{
			get { return (string) base[ElementPathsToExcludeChildrenFromSortingAlphabeticallyPropertyName]; }
			set
			{
				this._elementPathsToExcludeChildrenFromSortingAlphabetically = null;
				base[ElementPathsToExcludeChildrenFromSortingAlphabeticallyPropertyName] = value;
			}
		}

		public virtual ICollection<string> ElementPathsToInvolveChildElementWhenSortingElementsAlphabetically
		{
			get
			{
				if(this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically == null)
				{
					this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically = new EventHandledCollection<string>(this.Split(this.ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal));

					this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically.Added += (sender, arguments) => this.UpdateElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal(arguments);
					this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically.Cleared += (sender, arguments) => this.UpdateElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal(arguments);
					this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically.Removed += (sender, arguments) => this.UpdateElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal(arguments);
				}

				return this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically;
			}
		}

		[ConfigurationProperty(ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyPropertyName, IsRequired = false), StringValidator(InvalidCharacters = _invalidPathCharacters, MinLength = 0)]
		protected internal virtual string ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal
		{
			get { return (string) base[ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyPropertyName]; }
			set
			{
				this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically = null;
				base[ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyPropertyName] = value;
			}
		}

		[ConfigurationProperty(ElementsAlphabeticalSortDirectionPropertyName, DefaultValue = ListSortDirection.Ascending)]
		public virtual ListSortDirection ElementsAlphabeticalSortDirection
		{
			get { return (ListSortDirection) base[ElementsAlphabeticalSortDirectionPropertyName]; }
			set { base[ElementsAlphabeticalSortDirectionPropertyName] = value; }
		}

		[ConfigurationProperty(IndentPropertyName, DefaultValue = true)]
		public virtual bool Indent
		{
			get { return (bool) base[IndentPropertyName]; }
			set { base[IndentPropertyName] = value; }
		}

		[ConfigurationProperty(IndentStringPropertyName, DefaultValue = "\t")]
		public virtual string IndentString
		{
			get { return (string) base[IndentStringPropertyName]; }
			set { base[IndentStringPropertyName] = value; }
		}

		[ConfigurationProperty(InvolveAttributesWhenSortingElementsAlphabeticallyPropertyName, DefaultValue = false)]
		public virtual bool InvolveAttributesWhenSortingElementsAlphabetically
		{
			get { return (bool) base[InvolveAttributesWhenSortingElementsAlphabeticallyPropertyName]; }
			set { base[InvolveAttributesWhenSortingElementsAlphabeticallyPropertyName] = value; }
		}

		[ConfigurationProperty(MinimumNumberOfAttributesForNewLineOnAttributesPropertyName, DefaultValue = 0)]
		public virtual int MinimumNumberOfAttributesForNewLineOnAttributes
		{
			get { return (int) base[MinimumNumberOfAttributesForNewLineOnAttributesPropertyName]; }
			set { base[MinimumNumberOfAttributesForNewLineOnAttributesPropertyName] = value; }
		}

		public virtual string Name
		{
			get { return (string) this[NamePropertyName]; }
			set { this[NamePropertyName] = value; }
		}

		[ConfigurationProperty(NewLineOnAttributesPropertyName, DefaultValue = false)]
		public virtual bool NewLineOnAttributes
		{
			get { return (bool) base[NewLineOnAttributesPropertyName]; }
			set { base[NewLineOnAttributesPropertyName] = value; }
		}

		[ConfigurationProperty(NewLineStringPropertyName, DefaultValue = "\r\n")]
		public virtual string NewLineString
		{
			get { return (string) base[NewLineStringPropertyName]; }
			set { base[NewLineStringPropertyName] = value; }
		}

		[ConfigurationProperty(OmitCommentsPropertyName, DefaultValue = false)]
		public virtual bool OmitComments
		{
			get { return (bool) base[OmitCommentsPropertyName]; }
			set { base[OmitCommentsPropertyName] = value; }
		}

		[ConfigurationProperty(OmitXmlDeclarationPropertyName, DefaultValue = false)]
		public virtual bool OmitXmlDeclaration
		{
			get { return (bool) base[OmitXmlDeclarationPropertyName]; }
			set { base[OmitXmlDeclarationPropertyName] = value; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if(!this._initialized)
				{
					if(base.Properties.Contains(NamePropertyName))
						base.Properties.Remove(NamePropertyName);

					base.Properties.Add(this.CreateNameConfigurationProperty());

					this._initialized = true;
				}

				return base.Properties;
			}
		}

		[ConfigurationProperty(SortAttributesAlphabeticallyPropertyName, DefaultValue = false)]
		public virtual bool SortAttributesAlphabetically
		{
			get { return (bool) base[SortAttributesAlphabeticallyPropertyName]; }
			set { base[SortAttributesAlphabeticallyPropertyName] = value; }
		}

		[ConfigurationProperty(SortElementsAlphabeticallyPropertyName, DefaultValue = false)]
		public virtual bool SortElementsAlphabetically
		{
			get { return (bool) base[SortElementsAlphabeticallyPropertyName]; }
			set { base[SortElementsAlphabeticallyPropertyName] = value; }
		}

		#endregion

		#region Methods

		protected internal virtual ConfigurationProperty CreateNameConfigurationProperty()
		{
			return this.CreateNameConfigurationProperty(null, ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired, this.CreateNameConfigurationPropertyValidator());
		}

		protected internal virtual ConfigurationProperty CreateNameConfigurationProperty(object defaultValue, ConfigurationPropertyOptions options, ConfigurationValidatorBase validator)
		{
			return new ConfigurationProperty(NamePropertyName, typeof(string), defaultValue, null, validator, options);
		}

		protected internal virtual ConfigurationValidatorBase CreateNameConfigurationPropertyValidator()
		{
			return this.CreateNameConfigurationPropertyValidator(_invalidNameCharacters);
		}

		protected internal virtual ConfigurationValidatorBase CreateNameConfigurationPropertyValidator(string invalidNameCharacters)
		{
			return new StringValidator(1, 256, invalidNameCharacters);
		}

		protected internal virtual string Join<T>(CollectionEventArguments<T> arguments)
		{
			return arguments != null && arguments.Items != null ? string.Join(_joinSeparator, arguments.Items.Select(item => item.ToString()).ToArray()) : string.Empty;

			// When changing to .NET Framework 4.0 or higher you can change the above to:
			//return arguments != null && arguments.Items != null ? string.Join(_joinSeparator, arguments.Items) : string.Empty;
		}

		protected internal virtual IEnumerable<string> Split(string commaSeparatedValue)
		{
			return string.IsNullOrEmpty(commaSeparatedValue) ? new string[0] : commaSeparatedValue.Split(_splitSeparator.ToCharArray()).Select(item => item.Trim());
		}

		protected internal virtual void UpdateAttributeNamesToCorrectCommaSeparatedValuesForInternal(CollectionEventArguments<string> arguments)
		{
			this.AttributeNamesToCorrectCommaSeparatedValuesForInternal = this.Join(arguments);
		}

		protected internal virtual void UpdateAttributeNamesToPinFirstInternal(CollectionEventArguments<string> arguments)
		{
			this.AttributeNamesToPinFirstInternal = this.Join(arguments);
		}

		protected internal virtual void UpdateElementLevelsToExcludeFromSortingAlphabeticallyInternal(CollectionEventArguments<int> arguments)
		{
			this.ElementLevelsToExcludeFromSortingAlphabeticallyInternal = this.Join(arguments);
		}

		protected internal virtual void UpdateElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal(CollectionEventArguments<string> arguments)
		{
			this.ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal = this.Join(arguments);
		}

		protected internal virtual void UpdateElementNamesToPinFirstInternal(CollectionEventArguments<string> arguments)
		{
			this.ElementNamesToPinFirstInternal = this.Join(arguments);
		}

		protected internal virtual void UpdateElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal(CollectionEventArguments<string> arguments)
		{
			this.ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal = this.Join(arguments);
		}

		protected internal virtual void UpdateElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal(CollectionEventArguments<string> arguments)
		{
			this.ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal = this.Join(arguments);
		}

		#endregion
	}
}
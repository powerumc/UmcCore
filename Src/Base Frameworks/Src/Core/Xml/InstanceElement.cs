using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Collections;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Umc.Core.Xml
{


	internal class InstanceElement : InstanceGroup
	{

		bool isMixed = false;
		bool isNillable = false;
		bool genNil = true;
		StringBuilder comment;
		XmlQualifiedName xsiType = XmlQualifiedName.Empty;

		InstanceAttribute firstAttribute;

		internal InstanceElement ()
		{
		}

		internal InstanceElement (XmlQualifiedName name)
		{
			this.QualifiedName = new XmlQualifiedName(name.Name, name.Namespace);
			ValueGenerator = null;
		}


		internal bool IsMixed
		{
			get
			{
				return isMixed;
			}

			set
			{
				isMixed = value;
			}
		}

		internal bool IsNillable
		{
			get
			{
				return isNillable;
			}

			set
			{
				isNillable = value;
			}
		}

		internal bool GenNil
		{
			get
			{
				return genNil;
			}

			set
			{
				genNil = value;
			}
		}

		internal void AddAttribute (InstanceAttribute attr)
		{
			if ( firstAttribute == null )
			{
				firstAttribute = attr;
			}
			else
			{
				InstanceAttribute next = firstAttribute;
				InstanceAttribute prev = null;
				while ( next != null )
				{
					prev = next;
					next = next.NextAttribute;
				}
				prev.NextAttribute = attr;
			}
		}

		internal InstanceElement Clone (decimal occurs)
		{
			InstanceElement newElem = (InstanceElement)MemberwiseClone();
			newElem.Occurs = occurs;
			newElem.Child = null;
			newElem.Parent = null;
			newElem.Sibling = null;
			return newElem;
		}

		internal StringBuilder Comment
		{
			get
			{
				if ( comment == null )
				{
					comment = new StringBuilder();
				}
				return comment;
			}
			set
			{
				comment = value;
			}
		}

		internal XmlQualifiedName XsiType
		{
			get
			{
				return xsiType;
			}
			set
			{
				xsiType = value;
			}
		}
		internal InstanceAttribute FirstAttribute
		{
			get
			{
				return firstAttribute;
			}
		}
	}
}
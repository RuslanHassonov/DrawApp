﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DrawApp
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="2018_Hassonov_Ruslan_C#")]
	public partial class SQLServer_DrawAppDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTblColor(TblColor instance);
    partial void UpdateTblColor(TblColor instance);
    partial void DeleteTblColor(TblColor instance);
    partial void InsertTblShape(TblShape instance);
    partial void UpdateTblShape(TblShape instance);
    partial void DeleteTblShape(TblShape instance);
    partial void InsertTblDrawing(TblDrawing instance);
    partial void UpdateTblDrawing(TblDrawing instance);
    partial void DeleteTblDrawing(TblDrawing instance);
    partial void InsertTblOverview(TblOverview instance);
    partial void UpdateTblOverview(TblOverview instance);
    partial void DeleteTblOverview(TblOverview instance);
    #endregion
		
		public SQLServer_DrawAppDataContext() : 
				base(global::DrawApp.Properties.Settings.Default._2018_Hassonov_Ruslan_C_ConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TblColor> TblColors
		{
			get
			{
				return this.GetTable<TblColor>();
			}
		}
		
		public System.Data.Linq.Table<TblShape> TblShapes
		{
			get
			{
				return this.GetTable<TblShape>();
			}
		}
		
		public System.Data.Linq.Table<TblDrawing> TblDrawings
		{
			get
			{
				return this.GetTable<TblDrawing>();
			}
		}
		
		public System.Data.Linq.Table<TblOverview> TblOverviews
		{
			get
			{
				return this.GetTable<TblOverview>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblColor")]
	public partial class TblColor : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Color_ID;
		
		private System.Nullable<int> _Red;
		
		private System.Nullable<int> _Green;
		
		private System.Nullable<int> _Blue;
		
		private EntitySet<TblShape> _TblShapes;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnColor_IDChanging(int value);
    partial void OnColor_IDChanged();
    partial void OnRedChanging(System.Nullable<int> value);
    partial void OnRedChanged();
    partial void OnGreenChanging(System.Nullable<int> value);
    partial void OnGreenChanged();
    partial void OnBlueChanging(System.Nullable<int> value);
    partial void OnBlueChanged();
    #endregion
		
		public TblColor()
		{
			this._TblShapes = new EntitySet<TblShape>(new Action<TblShape>(this.attach_TblShapes), new Action<TblShape>(this.detach_TblShapes));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Color_ID
		{
			get
			{
				return this._Color_ID;
			}
			set
			{
				if ((this._Color_ID != value))
				{
					this.OnColor_IDChanging(value);
					this.SendPropertyChanging();
					this._Color_ID = value;
					this.SendPropertyChanged("Color_ID");
					this.OnColor_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Red", DbType="Int")]
		public System.Nullable<int> Red
		{
			get
			{
				return this._Red;
			}
			set
			{
				if ((this._Red != value))
				{
					this.OnRedChanging(value);
					this.SendPropertyChanging();
					this._Red = value;
					this.SendPropertyChanged("Red");
					this.OnRedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Green", DbType="Int")]
		public System.Nullable<int> Green
		{
			get
			{
				return this._Green;
			}
			set
			{
				if ((this._Green != value))
				{
					this.OnGreenChanging(value);
					this.SendPropertyChanging();
					this._Green = value;
					this.SendPropertyChanged("Green");
					this.OnGreenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Blue", DbType="Int")]
		public System.Nullable<int> Blue
		{
			get
			{
				return this._Blue;
			}
			set
			{
				if ((this._Blue != value))
				{
					this.OnBlueChanging(value);
					this.SendPropertyChanging();
					this._Blue = value;
					this.SendPropertyChanged("Blue");
					this.OnBlueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TblColor_TblShape", Storage="_TblShapes", ThisKey="Color_ID", OtherKey="Color_ID")]
		public EntitySet<TblShape> TblShapes
		{
			get
			{
				return this._TblShapes;
			}
			set
			{
				this._TblShapes.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TblShapes(TblShape entity)
		{
			this.SendPropertyChanging();
			entity.TblColor = this;
		}
		
		private void detach_TblShapes(TblShape entity)
		{
			this.SendPropertyChanging();
			entity.TblColor = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblShape")]
	public partial class TblShape : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Shape_ID;
		
		private string _Shape;
		
		private System.Nullable<int> _Width;
		
		private System.Nullable<int> _Height;
		
		private System.Nullable<int> _Color_ID;
		
		private EntitySet<TblDrawing> _TblDrawings;
		
		private EntityRef<TblColor> _TblColor;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnShape_IDChanging(int value);
    partial void OnShape_IDChanged();
    partial void OnShapeChanging(string value);
    partial void OnShapeChanged();
    partial void OnWidthChanging(System.Nullable<int> value);
    partial void OnWidthChanged();
    partial void OnHeightChanging(System.Nullable<int> value);
    partial void OnHeightChanged();
    partial void OnColor_IDChanging(System.Nullable<int> value);
    partial void OnColor_IDChanged();
    #endregion
		
		public TblShape()
		{
			this._TblDrawings = new EntitySet<TblDrawing>(new Action<TblDrawing>(this.attach_TblDrawings), new Action<TblDrawing>(this.detach_TblDrawings));
			this._TblColor = default(EntityRef<TblColor>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Shape_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Shape_ID
		{
			get
			{
				return this._Shape_ID;
			}
			set
			{
				if ((this._Shape_ID != value))
				{
					this.OnShape_IDChanging(value);
					this.SendPropertyChanging();
					this._Shape_ID = value;
					this.SendPropertyChanged("Shape_ID");
					this.OnShape_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Shape", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Shape
		{
			get
			{
				return this._Shape;
			}
			set
			{
				if ((this._Shape != value))
				{
					this.OnShapeChanging(value);
					this.SendPropertyChanging();
					this._Shape = value;
					this.SendPropertyChanged("Shape");
					this.OnShapeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Width", DbType="Int")]
		public System.Nullable<int> Width
		{
			get
			{
				return this._Width;
			}
			set
			{
				if ((this._Width != value))
				{
					this.OnWidthChanging(value);
					this.SendPropertyChanging();
					this._Width = value;
					this.SendPropertyChanged("Width");
					this.OnWidthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Height", DbType="Int")]
		public System.Nullable<int> Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				if ((this._Height != value))
				{
					this.OnHeightChanging(value);
					this.SendPropertyChanging();
					this._Height = value;
					this.SendPropertyChanged("Height");
					this.OnHeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color_ID", DbType="Int")]
		public System.Nullable<int> Color_ID
		{
			get
			{
				return this._Color_ID;
			}
			set
			{
				if ((this._Color_ID != value))
				{
					if (this._TblColor.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnColor_IDChanging(value);
					this.SendPropertyChanging();
					this._Color_ID = value;
					this.SendPropertyChanged("Color_ID");
					this.OnColor_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TblShape_TblDrawing", Storage="_TblDrawings", ThisKey="Shape_ID", OtherKey="Shape_ID")]
		public EntitySet<TblDrawing> TblDrawings
		{
			get
			{
				return this._TblDrawings;
			}
			set
			{
				this._TblDrawings.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TblColor_TblShape", Storage="_TblColor", ThisKey="Color_ID", OtherKey="Color_ID", IsForeignKey=true, DeleteRule="CASCADE")]
		public TblColor TblColor
		{
			get
			{
				return this._TblColor.Entity;
			}
			set
			{
				TblColor previousValue = this._TblColor.Entity;
				if (((previousValue != value) 
							|| (this._TblColor.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblColor.Entity = null;
						previousValue.TblShapes.Remove(this);
					}
					this._TblColor.Entity = value;
					if ((value != null))
					{
						value.TblShapes.Add(this);
						this._Color_ID = value.Color_ID;
					}
					else
					{
						this._Color_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("TblColor");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TblDrawings(TblDrawing entity)
		{
			this.SendPropertyChanging();
			entity.TblShape = this;
		}
		
		private void detach_TblDrawings(TblDrawing entity)
		{
			this.SendPropertyChanging();
			entity.TblShape = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblDrawing")]
	public partial class TblDrawing : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Drawing_ID;
		
		private System.Nullable<int> _Shape_ID;
		
		private System.Nullable<double> _X;
		
		private System.Nullable<double> _Y;
		
		private EntityRef<TblShape> _TblShape;
		
		private EntityRef<TblOverview> _TblOverview;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDrawing_IDChanging(int value);
    partial void OnDrawing_IDChanged();
    partial void OnShape_IDChanging(System.Nullable<int> value);
    partial void OnShape_IDChanged();
    partial void OnXChanging(System.Nullable<double> value);
    partial void OnXChanged();
    partial void OnYChanging(System.Nullable<double> value);
    partial void OnYChanged();
    #endregion
		
		public TblDrawing()
		{
			this._TblShape = default(EntityRef<TblShape>);
			this._TblOverview = default(EntityRef<TblOverview>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Drawing_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Drawing_ID
		{
			get
			{
				return this._Drawing_ID;
			}
			set
			{
				if ((this._Drawing_ID != value))
				{
					if (this._TblOverview.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDrawing_IDChanging(value);
					this.SendPropertyChanging();
					this._Drawing_ID = value;
					this.SendPropertyChanged("Drawing_ID");
					this.OnDrawing_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Shape_ID", DbType="Int")]
		public System.Nullable<int> Shape_ID
		{
			get
			{
				return this._Shape_ID;
			}
			set
			{
				if ((this._Shape_ID != value))
				{
					if (this._TblShape.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnShape_IDChanging(value);
					this.SendPropertyChanging();
					this._Shape_ID = value;
					this.SendPropertyChanged("Shape_ID");
					this.OnShape_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_X", DbType="Float")]
		public System.Nullable<double> X
		{
			get
			{
				return this._X;
			}
			set
			{
				if ((this._X != value))
				{
					this.OnXChanging(value);
					this.SendPropertyChanging();
					this._X = value;
					this.SendPropertyChanged("X");
					this.OnXChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Y", DbType="Float")]
		public System.Nullable<double> Y
		{
			get
			{
				return this._Y;
			}
			set
			{
				if ((this._Y != value))
				{
					this.OnYChanging(value);
					this.SendPropertyChanging();
					this._Y = value;
					this.SendPropertyChanged("Y");
					this.OnYChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TblShape_TblDrawing", Storage="_TblShape", ThisKey="Shape_ID", OtherKey="Shape_ID", IsForeignKey=true, DeleteRule="CASCADE")]
		public TblShape TblShape
		{
			get
			{
				return this._TblShape.Entity;
			}
			set
			{
				TblShape previousValue = this._TblShape.Entity;
				if (((previousValue != value) 
							|| (this._TblShape.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblShape.Entity = null;
						previousValue.TblDrawings.Remove(this);
					}
					this._TblShape.Entity = value;
					if ((value != null))
					{
						value.TblDrawings.Add(this);
						this._Shape_ID = value.Shape_ID;
					}
					else
					{
						this._Shape_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("TblShape");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TblOverview_TblDrawing", Storage="_TblOverview", ThisKey="Drawing_ID", OtherKey="Drawing_ID", IsForeignKey=true)]
		public TblOverview TblOverview
		{
			get
			{
				return this._TblOverview.Entity;
			}
			set
			{
				TblOverview previousValue = this._TblOverview.Entity;
				if (((previousValue != value) 
							|| (this._TblOverview.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblOverview.Entity = null;
						previousValue.TblDrawing = null;
					}
					this._TblOverview.Entity = value;
					if ((value != null))
					{
						value.TblDrawing = this;
						this._Drawing_ID = value.Drawing_ID;
					}
					else
					{
						this._Drawing_ID = default(int);
					}
					this.SendPropertyChanged("TblOverview");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblOverview")]
	public partial class TblOverview : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Drawing_ID;
		
		private string _Name;
		
		private System.Nullable<System.DateTime> _DateCreated;
		
		private System.Nullable<System.DateTime> _DateUpdated;
		
		private System.Nullable<int> _Shape_ID;
		
		private EntityRef<TblDrawing> _TblDrawing;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDrawing_IDChanging(int value);
    partial void OnDrawing_IDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDateCreatedChanging(System.Nullable<System.DateTime> value);
    partial void OnDateCreatedChanged();
    partial void OnDateUpdatedChanging(System.Nullable<System.DateTime> value);
    partial void OnDateUpdatedChanged();
    partial void OnShape_IDChanging(System.Nullable<int> value);
    partial void OnShape_IDChanged();
    #endregion
		
		public TblOverview()
		{
			this._TblDrawing = default(EntityRef<TblDrawing>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Drawing_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Drawing_ID
		{
			get
			{
				return this._Drawing_ID;
			}
			set
			{
				if ((this._Drawing_ID != value))
				{
					this.OnDrawing_IDChanging(value);
					this.SendPropertyChanging();
					this._Drawing_ID = value;
					this.SendPropertyChanged("Drawing_ID");
					this.OnDrawing_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateCreated", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateCreated
		{
			get
			{
				return this._DateCreated;
			}
			set
			{
				if ((this._DateCreated != value))
				{
					this.OnDateCreatedChanging(value);
					this.SendPropertyChanging();
					this._DateCreated = value;
					this.SendPropertyChanged("DateCreated");
					this.OnDateCreatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateUpdated", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateUpdated
		{
			get
			{
				return this._DateUpdated;
			}
			set
			{
				if ((this._DateUpdated != value))
				{
					this.OnDateUpdatedChanging(value);
					this.SendPropertyChanging();
					this._DateUpdated = value;
					this.SendPropertyChanged("DateUpdated");
					this.OnDateUpdatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Shape_ID", DbType="Int")]
		public System.Nullable<int> Shape_ID
		{
			get
			{
				return this._Shape_ID;
			}
			set
			{
				if ((this._Shape_ID != value))
				{
					this.OnShape_IDChanging(value);
					this.SendPropertyChanging();
					this._Shape_ID = value;
					this.SendPropertyChanged("Shape_ID");
					this.OnShape_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TblOverview_TblDrawing", Storage="_TblDrawing", ThisKey="Drawing_ID", OtherKey="Drawing_ID", IsUnique=true, IsForeignKey=false)]
		public TblDrawing TblDrawing
		{
			get
			{
				return this._TblDrawing.Entity;
			}
			set
			{
				TblDrawing previousValue = this._TblDrawing.Entity;
				if (((previousValue != value) 
							|| (this._TblDrawing.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TblDrawing.Entity = null;
						previousValue.TblOverview = null;
					}
					this._TblDrawing.Entity = value;
					if ((value != null))
					{
						value.TblOverview = this;
					}
					this.SendPropertyChanged("TblDrawing");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591

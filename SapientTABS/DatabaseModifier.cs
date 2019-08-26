using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using Landfall.TABS;

namespace SapientTABS
{
    public class DatabaseModifier
    {
        public static List<int> factionsCreatedNames = new List<int>();

        public void ModifyDatabase(LandfallUnitDatabase database)
        {
            // read json data structure from external file

            // use it to modify the database
            //database
        }

        public static void CreateNewFaction(LandfallUnitDatabase database, string name, Sprite icon)
        {
            Faction faction = new Faction();
            faction.Init();
            faction.Units = new UnitBlueprint[0];
            faction.Entity.Name = name;
            faction.Entity.SpriteIcon = icon;
            faction.name = name;
            faction.m_displayFaction = true;

            // make sure there isn't a faction by this name, otherwise simply return existing?
            if (!factionsCreatedNames.Contains(faction.Entity.GUID.m_ID))
            {
                factionsCreatedNames.Add(faction.Entity.GUID.m_ID);
                faction.index++; // += factionsCreatedNames.Count;

                //NOTE: I don't think we really need the following line, but it's a nice thought - 
                //database.Factions.Add(faction);
                database.AddFactionWithID(faction);
            }
        }
    }
}
/*
 Added to InitializeDatabase() of LandfallUnitDatabase.cs (in Assembly-CSharp.dll) just above call to UMH.CreateNewStuff(database);

				string str = "///////////////////////////////////////////////////////////////////////////////////////////////////////////UManager.WriteStuffDown();";
				try
				{
					JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects
					};
					jsonSerializerSettings.Converters.Add(new Vec2Conv());
					jsonSerializerSettings.Converters.Add(new Vec3Conv());
					jsonSerializerSettings.Converters.Add(new Vec4Conv());
					jsonSerializerSettings.Converters.Add(new ColorConverter());
					jsonSerializerSettings.Converters.Add(new DictionaryConverter());
					jsonSerializerSettings.Converters.Add(new Matrix4x4Converter());
					jsonSerializerSettings.Converters.Add(new QuaternionConverter());
					jsonSerializerSettings.Converters.Add(new BoundsConverter());
					jsonSerializerSettings.Converters.Add(new RectConverter());
					jsonSerializerSettings.Converters.Add(new RectOffsetConverter());
					jsonSerializerSettings.Converters.Add(new SpriteConverter());
					PropertyRenameAndIgnoreSerializerContractResolver propertyRenameAndIgnoreSerializerContractResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
					propertyRenameAndIgnoreSerializerContractResolver.IgnoreProperty(typeof(GameObject), new string[]
					{
						"transform"
					});
					jsonSerializerSettings.ContractResolver = propertyRenameAndIgnoreSerializerContractResolver;
					try
					{
						File.WriteAllText(Application.dataPath + "/data.json", JsonConvert.SerializeObject(database, Formatting.Indented, jsonSerializerSettings));
						try
						{
							string value = File.ReadAllText(Application.dataPath + "/data.json");
							database = JsonConvert.DeserializeObject<LandfallUnitDatabase>(value, jsonSerializerSettings);
						}
						catch (Exception ex)
						{
							File.WriteAllText(Application.dataPath + "/reading.data.json.errors.txt", string.Format("{0} :: {1} ::: {2}", ex.ToString(), ex.Message, ex.StackTrace));
						}
					}
					catch (Exception ex2)
					{
						File.WriteAllText(Application.dataPath + "/writing.data.json.errors.txt", string.Format("{0} :: {1} ::: {2}", ex2.ToString(), ex2.Message, ex2.StackTrace));
					}
				}
				catch (Exception ex3)
				{
					File.WriteAllText(Application.dataPath + "/errors.txt", string.Format("{0} :: {1} ::: {2}", ex3.ToString(), ex3.Message, ex3.StackTrace));
				}
				str += "////////////////////////////////////////////////////////////////////////////////";
     
*/

/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Landfall.TABS.Workshop;
using ModIO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Landfall.TABS
{
	// Token: 0x020003BD RID: 957
	[CreateAssetMenu(fileName = "Landfall Unit Database", menuName = "TABS/LandfallUnitDatabase", order = 1)]
	public class LandfallUnitDatabase : ScriptableObject
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000DEE RID: 3566
		public IEnumerable<IDatabaseEntity> UnitList
		{
			get
			{
				return this.m_unitDictionary.Values;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000DEF RID: 3567
		public IEnumerable<IDatabaseEntity> FactionList
		{
			get
			{
				File.AppendAllText(Application.dataPath + "/Factions.txt", "Getter for FactionList property");
				return this.m_factionDictionary.Values;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000DF0 RID: 3568
		public IEnumerable<IDatabaseEntity> TurningDataList
		{
			get
			{
				return this.TurningDatas;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000DF1 RID: 3569
		public IEnumerable<GameObject> UnitBaseList
		{
			get
			{
				return this.UnitBases;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000DF2 RID: 3570
		public IEnumerable<GameObject> WeaponList
		{
			get
			{
				return this.Weapons;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000DF3 RID: 3571
		public IEnumerable<GameObject> CombatMoveList
		{
			get
			{
				return this.CombatMoves;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000DF4 RID: 3572
		public IEnumerable<GameObject> CharacterPropList
		{
			get
			{
				return this.CharacterProps;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000DF5 RID: 3573
		public List<MapAsset> MapList
		{
			get
			{
				return this.Maps;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000DF6 RID: 3574
		public List<TABSCampaignAsset> LandfallCampaignList
		{
			get
			{
				return this.Campaigns;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000DF7 RID: 3575
		public List<TABSCampaignLevelAsset> LandfallCampaignLevelList
		{
			get
			{
				return this.CampaignLevels;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000DF8 RID: 3576
		public UpgradeDataAsset UpgradeData
		{
			get
			{
				return this.m_upgradeDataAsset;
			}
		}

		// Token: 0x06000DF9 RID: 3577
		public static LandfallUnitDatabase GetDatabase()
		{
			if (LandfallUnitDatabase.unitDatabase == null)
			{
				LandfallUnitDatabase.unitDatabase = Resources.Load<LandfallUnitDatabase>("Landfall Unit Database");
				LandfallUnitDatabase.unitDatabase.InitializeDatabase();
			}
			return LandfallUnitDatabase.unitDatabase;
		}

		// Token: 0x06000DFA RID: 3578
		private void InitializeDatabase()
		{
			for (int i = 0; i < this.Units.Count; i++)
			{
				this.AddUnitWithID(this.Units[i]);
			}
			for (int j = 0; j < this.Factions.Count; j++)
			{
				File.AppendAllText(Application.dataPath + "/Factions.txt", string.Format("Inside for...loop using AddFactionWithID() at the top of InitializeDatabase() :: {0}", this.Factions.Count));
				this.AddFactionWithID(this.Factions[j]);
			}
			for (int k = 0; k < this.Campaigns.Count; k++)
			{
				this.Campaigns[k].IsLandfallLevel = true;
				this.AddCampaignWithID(this.Campaigns[k]);
			}
			for (int l = 0; l < this.CampaignLevels.Count; l++)
			{
				this.AddCampaignLevelWithID(this.CampaignLevels[l]);
			}
			try
			{
				LandfallUnitDatabase database = LandfallUnitDatabase.GetDatabase();
				Resources.FindObjectsOfTypeAll<Faction>().ToList<Faction>().ForEach(delegate(Faction f)
				{
					if (!database.Factions.Contains(f))
					{
						LandfallUnitDatabase.GetDatabase().Factions.Add(f);
						LandfallUnitDatabase.GetDatabase().AddFactionWithID(f);
					}
				});
				foreach (WeaponItem weaponItem in Resources.FindObjectsOfTypeAll<WeaponItem>())
				{
					DatabaseID guid = weaponItem.Entity.GUID;
					if (!database.Weapons.Contains(weaponItem.gameObject) && weaponItem.name == "GiantSlap" && weaponItem.gameObject != null)
					{
						database.Weapons.Add(weaponItem.gameObject);
					}
				}
				foreach (CombatMove combatMove in Resources.FindObjectsOfTypeAll<CombatMove>())
				{
					DatabaseID guid2 = combatMove.Entity.GUID;
					if (!database.CombatMoves.Contains(combatMove.gameObject) && combatMove.gameObject != null)
					{
						database.CombatMoves.Add(combatMove.gameObject);
					}
				}
				Resources.FindObjectsOfTypeAll<PropItem>().ToList<PropItem>().ForEach(delegate(PropItem c)
				{
					if (!database.CharacterProps.Contains(c.gameObject) && c.gameObject != null)
					{
						database.CharacterProps.Add(c.gameObject);
					}
				});
				UManager.WriteStuffDown();
				UMH.CreateNewStuff(database);
				UMH.SetupCustomUnits(database);
				Resources.FindObjectsOfTypeAll<UnitBlueprint>().ToList<UnitBlueprint>().ForEach(delegate(UnitBlueprint u)
				{
					if (!database.Units.Contains(u))
					{
						database.AddUnitWithID(u);
						database.Units.Add(u);
					}
					UManager.RequiredVariables(u.Entity.GUID);
				});
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000DFB RID: 3579
		private bool AddEntityWithID<T>(T entity, Dictionary<DatabaseID, T> dictionary) where T : IDatabaseEntity
		{
			if (dictionary.ContainsKey(entity.Entity.GUID))
			{
				dictionary[entity.Entity.GUID] = entity;
				return false;
			}
			dictionary.Add(entity.Entity.GUID, entity);
			return true;
		}

		// Token: 0x06000DFC RID: 3580
		public void AddUnitWithID(UnitBlueprint unit)
		{
			this.AddEntityWithID<UnitBlueprint>(unit, this.m_unitDictionary);
		}

		// Token: 0x06000DFD RID: 3581
		public void AddFactionWithID(Faction faction)
		{
			File.AppendAllText(Application.dataPath + "/Factions.txt", string.Format("Inside AddFactionWithID() :: {0}", this.m_factionDictionary.Count));
			this.AddEntityWithID<Faction>(faction, this.m_factionDictionary);
		}

		// Token: 0x06000DFE RID: 3582
		public void AddCampaignWithID(TABSCampaignAsset campaign)
		{
			this.AddEntityWithID<TABSCampaignAsset>(campaign, this.m_campaignDictionary);
		}

		// Token: 0x06000DFF RID: 3583
		public void AddCampaignLevelWithID(TABSCampaignLevelAsset campaignLevel)
		{
			if (!this.AddEntityWithID<TABSCampaignLevelAsset>(campaignLevel, this.m_campaignLevelDictionary))
			{
				this.RefreshCampaignLevels(campaignLevel);
			}
		}

		// Token: 0x06000E00 RID: 3584
		public void RemoveCampaignLevelWithGUID(DatabaseID id)
		{
			if (this.m_campaignLevelDictionary.ContainsKey(id))
			{
				this.m_campaignLevelDictionary.Remove(id);
			}
		}

		// Token: 0x06000E01 RID: 3585
		public void RemoveCampaignWithGUID(DatabaseID id)
		{
			if (this.m_campaignDictionary.ContainsKey(id))
			{
				this.m_campaignDictionary.Remove(id);
			}
		}

		// Token: 0x06000E02 RID: 3586
		private void RefreshCampaignLevels(TABSCampaignLevelAsset campaignLevel)
		{
			foreach (KeyValuePair<DatabaseID, TABSCampaignAsset> keyValuePair in this.m_campaignDictionary)
			{
				if (keyValuePair.Value.IsCustomCampaign)
				{
					for (int i = 0; i < keyValuePair.Value.LevelsInCampaign.Length; i++)
					{
						if (!(keyValuePair.Value.LevelsInCampaign[i] == null) && !(campaignLevel.Entity.GUID != keyValuePair.Value.LevelsInCampaign[i].Entity.GUID))
						{
							keyValuePair.Value.LevelsInCampaign[i] = campaignLevel;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000E03 RID: 3587
		public UnitBlueprint GetUnitByGUID(DatabaseID id)
		{
			if (!this.m_unitDictionary.ContainsKey(id))
			{
				return null;
			}
			return this.m_unitDictionary[id];
		}

		// Token: 0x06000E04 RID: 3588
		public Faction GetFactionByGUID(DatabaseID id)
		{
			if (!this.m_factionDictionary.ContainsKey(id))
			{
				return null;
			}
			return this.m_factionDictionary[id];
		}

		// Token: 0x06000E05 RID: 3589
		public bool HasCampaign(DatabaseID id)
		{
			return this.m_campaignDictionary.ContainsKey(id);
		}

		// Token: 0x06000E06 RID: 3590
		public TABSCampaignLevelAsset GetCampaignLevelByName(string levelName, WorkshopTypeFilter filter)
		{
			if (filter == WorkshopTypeFilter.Local)
			{
				foreach (KeyValuePair<DatabaseID, TABSCampaignLevelAsset> keyValuePair in this.m_campaignLevelDictionary)
				{
					TABSCampaignLevelAsset value = keyValuePair.Value;
					if (!value.IsModIOLevel && value.Entity.Name.ToLower().Equals(levelName.ToLower()))
					{
						return value;
					}
				}
			}
			return null;
		}

		// Token: 0x06000E07 RID: 3591
		public TABSCampaignAsset GetCampaignByName(string campaignName, WorkshopTypeFilter filter)
		{
			if (filter == WorkshopTypeFilter.Local)
			{
				foreach (KeyValuePair<DatabaseID, TABSCampaignAsset> keyValuePair in this.m_campaignDictionary)
				{
					TABSCampaignAsset value = keyValuePair.Value;
					if (!value.IsModCampaign && value.Entity.Name.ToLower().Equals(campaignName.ToLower()))
					{
						return value;
					}
				}
			}
			return null;
		}

		// Token: 0x06000E08 RID: 3592
		public TABSCampaignAsset GetCampaignByGUID(DatabaseID id)
		{
			if (!this.m_campaignDictionary.ContainsKey(id))
			{
				return null;
			}
			return this.m_campaignDictionary[id];
		}

		// Token: 0x06000E09 RID: 3593
		public bool HasCampaignLevel(DatabaseID id)
		{
			return this.m_campaignLevelDictionary.ContainsKey(id);
		}

		// Token: 0x06000E0A RID: 3594
		public TABSCampaignLevelAsset GetCampaignLevelByGUID(DatabaseID id)
		{
			if (!this.m_campaignLevelDictionary.ContainsKey(id))
			{
				return null;
			}
			return this.m_campaignLevelDictionary[id];
		}

		// Token: 0x06000E0B RID: 3595
		public UnitBlueprint[] GetCustomUnits()
		{
			return (from unit in this.m_unitDictionary.Values
			where unit.IsCustomUnit
			select unit).ToArray<UnitBlueprint>();
		}

		// Token: 0x06000E0C RID: 3596
		public TABSCampaignAsset[] GetCustomCampaigns(bool excludeDisabled = true)
		{
			Dictionary<DatabaseID, TABSCampaignAsset>.ValueCollection values = this.m_campaignDictionary.Values;
			if (excludeDisabled)
			{
				List<int> enabledMods = ModManager.GetEnabledModIds();
				return (from level in values
				where level.IsCustomCampaign && (enabledMods.Contains(level.ModID) || !level.IsModCampaign)
				select level).ToArray<TABSCampaignAsset>();
			}
			return (from level in values
			where level.IsCustomCampaign
			select level).ToArray<TABSCampaignAsset>();
		}

		// Token: 0x06000E0D RID: 3597
		public TABSCampaignLevelAsset[] GetCustomCampaignLevels(bool excludeDisabled = true)
		{
			Dictionary<DatabaseID, TABSCampaignLevelAsset>.ValueCollection values = this.m_campaignLevelDictionary.Values;
			if (excludeDisabled)
			{
				List<int> enabledMods = ModManager.GetEnabledModIds();
				return (from level in values
				where level.IsCustomCampaignLevel && (enabledMods.Contains(level.ModID) || !level.IsModIOLevel)
				select level).ToArray<TABSCampaignLevelAsset>();
			}
			return (from level in values
			where level.IsCustomCampaignLevel
			select level).ToArray<TABSCampaignLevelAsset>();
		}

		// Token: 0x06000E0E RID: 3598
		public Faction[] GetFactions()
		{
			return (from x in this.m_factionDictionary.Values
			orderby x.index
			select x).ToArray<Faction>();
		}

		// Token: 0x06000E0F RID: 3599
		public MapAsset GetMap(int index)
		{
			return this.Maps[index];
		}

		// Token: 0x06000E10 RID: 3600
		public MapAsset GetMap(DatabaseID id)
		{
			if (id == default(DatabaseID))
			{
				return this.Maps[0];
			}
			foreach (MapAsset mapAsset in this.Maps)
			{
				if (mapAsset.Entity.GUID == id)
				{
					return mapAsset;
				}
			}
			throw new Exception("No Map WIth GUID: " + id + " Could be found in the database");
		}

		// Token: 0x06000E11 RID: 3601
		public string[] GetFactionNames()
		{
			string[] array = new string[this.Factions.Count];
			for (int i = 0; i < this.Factions.Count; i++)
			{
				array[i] = this.Factions[i].name;
			}
			return array;
		}

		// Token: 0x06000E12 RID: 3602
		public void RemoveFaction(Faction faction)
		{
			for (int i = 0; i < this.Factions.Count; i++)
			{
				if (faction == this.Factions[i])
				{
					this.Factions.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000E13 RID: 3603
		public void RemoveUnit(UnitBlueprint unit)
		{
			for (int i = 0; i < this.Units.Count; i++)
			{
				if (unit == this.Units[i])
				{
					this.Units.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000E14 RID: 3604
		public List<CharacterItem> GetPropsOfType(UnitRig.GearType gearType)
		{
			List<CharacterItem> list = new List<CharacterItem>();
			int count = this.CharacterProps.Count;
			for (int i = 0; i < count; i++)
			{
				CharacterItem component = this.CharacterProps[i].GetComponent<CharacterItem>();
				if (!(component == null) && component.GearT == gearType && component.ShowInEditor)
				{
					list.Add(component);
				}
			}
			return list;
		}

		// Token: 0x06000E15 RID: 3605
		public List<CharacterItem> GetWeaponsOfType<T>() where T : Weapon
		{
			List<CharacterItem> list = new List<CharacterItem>();
			int count = this.Weapons.Count;
			for (int i = 0; i < count; i++)
			{
				Weapon weapon = this.Weapons[i].GetComponent<T>();
				if (!(weapon == null))
				{
					CharacterItem component = weapon.GetComponent<WeaponItem>();
					if (component.ShowInEditor)
					{
						list.Add(component);
					}
				}
			}
			return list;
		}

		// Token: 0x06000E16 RID: 3606
		public LandfallUnitDatabase()
		{
			this.UnitBases = new List<GameObject>();
			this.CombatMoves = new List<GameObject>();
			this.CharacterProps = new List<GameObject>();
			this.Weapons = new List<GameObject>();
			this.TurningDatas = new List<TurningData>();
			this.Maps = new List<MapAsset>();
			this.Campaigns = new List<TABSCampaignAsset>();
			this.CampaignLevels = new List<TABSCampaignLevelAsset>();
			this.m_unitDictionary = new Dictionary<DatabaseID, UnitBlueprint>();
			this.m_factionDictionary = new Dictionary<DatabaseID, Faction>();
			this.m_campaignDictionary = new Dictionary<DatabaseID, TABSCampaignAsset>();
			this.m_campaignLevelDictionary = new Dictionary<DatabaseID, TABSCampaignLevelAsset>();
			base..ctor();
			File.AppendAllText(Application.dataPath + "/Factions.txt", "Default .ctor");
		}

		// Token: 0x06000E17 RID: 3607
		static LandfallUnitDatabase()
		{
			File.AppendAllText(Application.dataPath + "/Factions.txt", "Default static .ctor");
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600168D RID: 5773
		// (set) Token: 0x0600168E RID: 5774
		[SerializeField]
		public List<Faction> Factions
		{
			get
			{
				File.AppendAllText(Application.dataPath + "/Factions.txt", "In the getter for Factions property");
				return this.ShadowFactionList;
			}
			set
			{
				File.AppendAllText(Application.dataPath + "/Factions.txt", string.Format("In the setter for Factions property:: {0}", (value != null) ? value.Count.ToString() : string.Empty));
				this.ShadowFactionList = value;
			}
		}

		// Token: 0x040012BD RID: 4797
		public string m_version = "0.1.0";

		// Token: 0x040012BE RID: 4798
		public bool m_isEarlyAccess;

		// Token: 0x040012BF RID: 4799
		[SerializeField]
		public List<UnitBlueprint> Units = new List<UnitBlueprint>();

		// Token: 0x040012C1 RID: 4801
		[SerializeField]
		public List<GameObject> UnitBases;

		// Token: 0x040012C2 RID: 4802
		[SerializeField]
		public List<GameObject> CombatMoves;

		// Token: 0x040012C3 RID: 4803
		[SerializeField]
		public List<GameObject> CharacterProps;

		// Token: 0x040012C4 RID: 4804
		[SerializeField]
		public List<GameObject> Weapons;

		// Token: 0x040012C5 RID: 4805
		[SerializeField]
		public List<TurningData> TurningDatas;

		// Token: 0x040012C6 RID: 4806
		[SerializeField]
		public List<MapAsset> Maps;

		// Token: 0x040012C7 RID: 4807
		[SerializeField]
		public List<TABSCampaignAsset> Campaigns;

		// Token: 0x040012C8 RID: 4808
		[SerializeField]
		public List<TABSCampaignLevelAsset> CampaignLevels;

		// Token: 0x040012C9 RID: 4809
		public static readonly string unitPath = "Assets/2 Units/";

		// Token: 0x040012CA RID: 4810
		public static readonly string factionsPath = "Assets/2 Units/Factions/";

		// Token: 0x040012CB RID: 4811
		public static readonly string unitBasePath = "Assets/1 Prefabs/0 UnitBases/";

		// Token: 0x040012CC RID: 4812
		public static readonly string combatMovesPath = "Assets/1 Prefabs/4 Moves/";

		// Token: 0x040012CD RID: 4813
		public static readonly string propPath = "Assets/1 Prefabs/8 CharacterProps/";

		// Token: 0x040012CE RID: 4814
		public static readonly string weaponPath = "Assets/1 Prefabs/1 Weapons/";

		// Token: 0x040012CF RID: 4815
		public static readonly string turningDataPath = "Assets/2 Units/Data/";

		// Token: 0x040012D0 RID: 4816
		public static readonly string iconsPath = "Assets/2 Units/Icons/";

		// Token: 0x040012D1 RID: 4817
		public static readonly string mapsPath = "Assets/12 Maps/";

		// Token: 0x040012D2 RID: 4818
		public static readonly string campaignsPath = "Assets/13 Campaigns/";

		// Token: 0x040012D3 RID: 4819
		public UnitEditorColorPalette colorPalette;

		// Token: 0x040012D4 RID: 4820
		[FoldoutGroup("Default New Unit Values", 0)]
		public UnitBlueprint m_unitEditorBlueprint;

		// Token: 0x040012D5 RID: 4821
		[FoldoutGroup("Default New Unit Values", 0)]
		public GameObject defaultUnitBase;

		// Token: 0x040012D6 RID: 4822
		[FoldoutGroup("Default New Unit Values", 0)]
		public TurningData defaultTurningData;

		// Token: 0x040012D7 RID: 4823
		[SerializeField]
		public UpgradeDataAsset m_upgradeDataAsset;

		// Token: 0x040012D8 RID: 4824
		public Dictionary<DatabaseID, UnitBlueprint> m_unitDictionary;

		// Token: 0x040012D9 RID: 4825
		public Dictionary<DatabaseID, Faction> m_factionDictionary;

		// Token: 0x040012DA RID: 4826
		public Dictionary<DatabaseID, TABSCampaignAsset> m_campaignDictionary;

		// Token: 0x040012DB RID: 4827
		public Dictionary<DatabaseID, TABSCampaignLevelAsset> m_campaignLevelDictionary;

		// Token: 0x040012DC RID: 4828
		private static LandfallUnitDatabase unitDatabase;

		// Token: 0x04001A88 RID: 6792
		private List<Faction> ShadowFactionList = new List<Faction>();
	}
}
 
*/

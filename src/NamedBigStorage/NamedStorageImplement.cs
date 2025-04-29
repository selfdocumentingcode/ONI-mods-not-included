// Decompiled with JetBrains decompiler
// Type: NamedStorage.NamedStorageImplement
// Assembly: NamedStorage, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 4FBF6D13-39C0-4212-95AB-5935240514FB
// Assembly location: %USERPROFILE%\Documents\Klei\OxygenNotIncluded\mods\Steam\2991018571\NamedStorage.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NamedBigStorage
{
    internal class NamedStorageImplement : KMonoBehaviour
    {
        [MyCmpGet] private Storage storage;

        public bool SetReplaceNameStorage()
        {
            string name = storage.gameObject.GetComponent<Building>()?.Def?.Name;
            bool oneElement;
            Tag tag = GetTag(out oneElement);
            if (tag != Tag.Invalid)
                name = $"[{tag.ProperNameStripLink()}]{name}";
            UserNameable component = storage.gameObject.GetComponent<UserNameable>();
            if (component != null)
                component.SetName(name);
            else
                SetName(name);
            return oneElement;
        }

        private void SetName(string name)
        {
            KSelectable component = storage.GetComponent<KSelectable>();
            this.name = name;
            if (component != null)
                component.SetName(name);
            storage.gameObject.name = name;
            NameDisplayScreen.Instance.UpdateName(storage.gameObject);
            if (GetComponent<CommandModule>() != null)
                SpacecraftManager.instance
                    .GetSpacecraftFromLaunchConditionManager(GetComponent<LaunchConditionManager>())
                    .SetRocketName(name);
            else if (GetComponent<Clustercraft>() != null)
                ClusterNameDisplayScreen.Instance.UpdateName(GetComponent<Clustercraft>());
            Trigger(1102426921, name);
        }

        private Tag GetTag(out bool oneElement)
        {
            oneElement = true;
            HashSet<Tag> tags = storage.gameObject.GetComponent<TreeFilterable>()?.GetTags();
            if (tags.Count == 0 || tags.Contains(GameTags.Any))
                return Tag.Invalid;
            if (tags.Count == 1)
                return tags.First<Tag>();
            oneElement = false;
            foreach (Tag allCategory in GameTags.AllCategories)
            {
                HashSet<Tag> resources;
                if (DiscoveredResources.Instance.TryGetDiscoveredResourcesFromTag(allCategory, out resources) &&
                    tags.SetEquals(resources))
                    return allCategory;
            }

            return Tag.Invalid;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            SetReplaceNameStorage();
        }
    }
}
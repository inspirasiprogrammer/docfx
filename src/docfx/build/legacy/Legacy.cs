// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Docs.Build
{
    internal static class Legacy
    {
        public static void ConvertToLegacyModel(
            Docset docset,
            Context context,
            Dictionary<FilePath, PublishItem> fileManifests,
            DependencyMap dependencyMap)
        {
            using (Progress.Start("Converting to legacy"))
            {
                var documents = fileManifests.Where(f => !f.Value.HasError).ToDictionary(
                    k => context.DocumentProvider.GetDocument(k.Key), v => v.Value);

                LegacyManifest.Convert(docset, context, documents);
                var legacyDependencyMap = LegacyDependencyMap.Convert(docset, context, documents.Keys.ToList(), dependencyMap);
                LegacyFileMap.Convert(context, legacyDependencyMap, documents);
            }
        }
    }
}

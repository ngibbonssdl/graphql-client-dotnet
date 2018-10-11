﻿using System.Collections.Generic;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api
    /// </summary>
    public interface IPublicContentApi
    {
        IContextData GlobalContextData { get; set; }

        Page GetPage(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        Page GetPage(ContentNamespace ns, int publicationId, string url, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        Page GetPage(ContentNamespace ns, int publicationId, CmUri cmUri, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId, string customMetaFilter,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            IContextData contextData);

        BinaryComponent GetBinaryComponent(CmUri cmUri, string customMetaFilter, IContextData contextData);

        ItemConnection ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, bool includeContainerItems, IContextData contextData);

        Publication GetPublication(ContentNamespace ns, int publicationId, string customMetaFilter,
            IContextData contextData);

        PublicationConnection GetPublications(ContentNamespace ns, IPagination pagination, InputPublicationFilter filter,
            string customMetaFilter,
            IContextData contextData);

        string ResolvePageLink(ContentNamespace ns, int publicationId, int pageId, bool renderRelativeLink);

        string ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, int? sourcePageId,
            int? excludeComponentTemplateId, bool renderRelativeLink);

        string ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId, string variantId,
            bool renderRelativeLink);

        string ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
            int templateId, bool renderRelativeLink);

        PublicationMapping GetPublicationMapping(ContentNamespace ns, string url);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData);

        dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData);

        dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
            ContentType contentType,
            DataModelType modelType, DcpType dcpType, ContentIncludeMode contentIncludeMode, IContextData contextData);

        TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData);

        List<TaxonomySitemapItem> GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, Ancestor ancestor,
            IContextData contextData);
    }
}
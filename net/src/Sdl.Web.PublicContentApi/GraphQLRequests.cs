﻿using System;
using System.Linq;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Predefined GraphQL requests for working with the Public Content Api
    /// </summary>
    public static class GraphQLRequests
    {
        public static IGraphQLRequest Page(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageById", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithPageId(pageId)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest Page(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageByUrl", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithUrl(url)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest Page(CmUri cmUri, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageByCmUri", true).WithCmUri(cmUri)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest BinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("BinaryComponentById", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithBinaryId(binaryId)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest BinaryComponent(ContentNamespace ns, int publicationId, string url,
            string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("BinaryComponentByUrl", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithUrl(url)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest BinaryComponent(CmUri cmUri, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("BinaryComponentByCmUri", true).WithCmUri(cmUri)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest ExecuteItemQuery(InputItemFilter filter, InputSortParam sort,
            IPagination pagination, string customMetaFilter, bool renderContent, bool includeContainerItems,
            IContextData contextData, IContextData globaContextData)
        {
            QueryBuilder builder = new QueryBuilder().WithQueryResource("ItemQuery", false);

            // We only include the fragments that will be required based on the item types in the
            // input item filter
            if (filter.ItemTypes != null)
            {
                string fragmentList = filter.ItemTypes.Select(itemType
                    => $"{Enum.GetName(typeof (ContentModel.FilterItemType), itemType).Capitialize()}Fields")
                    .Aggregate(string.Empty, (current, fragment) => current + $"...{fragment}\n");
                // Just a quick and easy way to replace markers in our queries with vars here.
                builder.ReplaceTag("fragmentList", fragmentList);
                builder.LoadFragments();
            }

            return builder.WithIncludeRegion("includeContainerItems", includeContainerItems).
                WithPagination(pagination).
                WithCustomMetaFilter(customMetaFilter).
                WithRenderContent(renderContent).
                WithInputItemFilter(filter).
                WithInputSortParam(sort).
                WithContextData(contextData).
                WithContextData(globaContextData).
                WithConvertor(new ItemConvertor()).
                Build();
        }

        public static IGraphQLRequest Publication(ContentNamespace ns, int publicationId, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("Publication", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest Publications(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter filter, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("Publications", true).WithNamespace(ns).WithPagination(pagination)
                    .WithInputPublicationFilter(filter)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest ResolvePageLink(ContentNamespace ns, int publicationId, int pageId,
            bool renderRelativeLink)
            => new QueryBuilder().WithQueryResource("ResolvePageLink", true)
                .WithNamespace(ns)
                .WithPublicationId(publicationId)
                .WithPageId(pageId)
                .WithRenderRelativeLink(renderRelativeLink).Build();

        public static IGraphQLRequest ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId,
            int? sourcePageId,
            int? excludeComponentTemplateId, bool renderRelativeLink) =>
                new QueryBuilder().WithQueryResource("ResolveComponentLink", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .
                    WithRenderRelativeLink(renderRelativeLink)
                    .WithVariable("targetComponentId", componentId)
                    .WithVariable("sourcePageId", sourcePageId)
                    .WithVariable("excludeComponentTemplateId", excludeComponentTemplateId)
                    .Build();

        public static IGraphQLRequest ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
            string variantId, bool renderRelativeLink) =>
                new QueryBuilder().WithQueryResource("ResolveBinaryLink", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .
                    WithRenderRelativeLink(renderRelativeLink)
                    .WithBinaryId(binaryId)
                    .WithVariable("variantId", variantId)
                    .Build();

        public static IGraphQLRequest ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId,
            int componentId,
            int templateId, bool renderRelativeLink) =>
                new QueryBuilder().WithQueryResource("ResolveDynamicComponentLink", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .
                    WithRenderRelativeLink(renderRelativeLink)
                    .WithVariable("targetPageId", pageId)
                    .WithVariable("targetComponentId", componentId)
                    .
                    WithVariable("targetTemplateId", templateId)
                    .Build();

        public static IGraphQLRequest PublicationMapping(ContentNamespace ns, string url) =>
            new QueryBuilder().WithQueryResource("PublicationMapping", true)
                .WithNamespace(ns)
                .WithVariable("siteUrl", url)
                .Build();

        public static IGraphQLRequest PageModelData(ContentNamespace ns, int publicationId, int pageId,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageModelById", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithPageId(pageId)
                    .WithRenderContent(renderContent)
                    .WithContextClaim(CreateClaim(contentType))
                    .WithContextClaim(CreateClaim(modelType))
                    .WithContextClaim(CreateClaim(pageInclusion))
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest PageModelData(ContentNamespace ns, int publicationId, string url,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            IContextData globalContextData) =>

                new QueryBuilder().WithQueryResource("PageModelByUrl", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithUrl(url)
                    .WithRenderContent(renderContent)
                    .WithContextClaim(CreateClaim(contentType))
                    .WithContextClaim(CreateClaim(modelType))
                    .WithContextClaim(CreateClaim(pageInclusion))
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest EntityModelData(ContentNamespace ns, int publicationId, int entityId,
            int templateId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData,
            IContextData globalContextData) =>

                new QueryBuilder().WithQueryResource("EntityModelById", true).
                    WithNamespace(ns).
                    WithPublicationId(publicationId).
                    WithVariable("componentId", entityId).
                    WithVariable("templateId", templateId).
                    WithRenderContent(renderContent).
                    WithContextClaim(CreateClaim(contentType)).
                    WithContextClaim(CreateClaim(modelType)).
                    WithContextClaim(CreateClaim(dcpType)).
                    WithContextData(contextData).
                    WithContextData(globalContextData).
                    Build();

        public static IGraphQLRequest Sitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData, IContextData globalContextData)
            => new QueryBuilder().WithQueryResource("Sitemap", true)
                .WithNamespace(ns)
                .WithPublicationId(publicationId)
                .WithDescendantLevels(descendantLevels)
                .WithContextData(contextData)
                .WithContextData(globalContextData)
                .WithConvertor(new TaxonomyItemConvertor())
                .Build();

        public static IGraphQLRequest SitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, bool includeAncestors,
            IContextData contextData, IContextData globalContextData)
        {
            QueryBuilder builder =
                new QueryBuilder().WithQueryResource(
                    descendantLevels == 0 ? "SitemapSubtreeNoRecurse" : "SitemapSubtree", true);
            return
                builder.WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithVariable("taxonomyNodeId", taxonomyNodeId)
                    .WithVariable("includeAncestors", includeAncestors)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .WithDescendantLevels(descendantLevels)
                    .WithConvertor(new TaxonomyItemConvertor())
                    .Build();
        }

        #region Query Builder Helpers

        private static ClaimValue CreateClaim(ContentType contentType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ContentType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (ContentType), contentType)
        };

        private static ClaimValue CreateClaim(DataModelType dataModelType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DataModelType), dataModelType)
        };

        private static ClaimValue CreateClaim(PageInclusion pageInclusion) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (PageInclusion), pageInclusion)
        };

        private static ClaimValue CreateClaim(DcpType dcpType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.EntityDcpType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DcpType), dcpType)
        };

        #endregion
    }
}
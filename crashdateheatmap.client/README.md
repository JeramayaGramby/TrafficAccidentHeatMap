# Crash Date Heat Map

## Overview

The Crash Date Heat Map project visualizes historical traffic collisions as a geographic heat map where "hotter" areas indicate higher collision frequency. The repository contains CSV data collected for West Mifflin and surrounding Pittsburgh neighborhoods and an ASP.NET backend to process and serve the data for client-side mapping.

## Motivation

West Mifflin sits at the nexus of many Pittsburgh neighborhoods and regional routes. 
The area has experienced growth and redevelopment while continuing to show poorly designed traffic zones (for example, the Century III area). 
The goal of this project is to identify high-crash zones, surface patterns, and potential root causes so findings can be presented to local Town Hall meetings or transportation planners.

## Goals

- Aggregate and normalize historical traffic and accident CSVs into a consistent dataset.
- Render a scalable, interactive heat map where intensity corresponds to collision frequency.
- Support later filters (fatalities, speeding, time-of-day, vehicle type) and temporal analysis.
- Provide reproducible processing in an ASP.NET project so data and visualizations can be hosted locally or deployed.

## Data

Raw CSV files belong in the `data/` directory of the repository. Below are the recommended columns for ingestion:

| Column | Type | Description |
|---|---:|---|
| `id` | string/int | Unique identifier for the event |
| `latitude` | float | Latitude in decimal degrees |
| `longitude` | float | Longitude in decimal degrees |
| `occurred_at` | datetime | Date and time of the collision (ISO8601 recommended) |
| `severity` | string/int | Severity label or code (e.g., `fatal`, `injury`, `property`) |
| `collision_type` | string | Optional: `speeding`, `pedestrian`, `bicycle`, etc. |

The data ingestion routines I developed will normalize column names and types, deduplicate events, and export a format suitable for visualization in Angular.

> **Note**: Remove or obfuscate any personally identifying information before sharing data publicly.

## Scale of the Data

When fully complete, this pipeline will manage around several hundreds of thousands of rows. Spatial binning (grid, hex, or geohash) and temporal bucketing (hour/day/week) are used to compute intensities for efficient client rendering.

## Core Technologies and Framework

- ASP.NET8 (`CrashDateHeatMap.Server`) — REST API backend and data-processing service. It handles CSV ingestion, normalization, aggregation (tiling/bucketing), and serves endpoints that the frontend consumes.
- Angular (`crashdateheatmap.client`) — Single Page Application (TypeScript) that consumes the ASP.NET API and renders interactive map visualizations and UI controls for filtering and exploration.
- Mapping libraries (client) — `Leaflet` or `Mapbox GL JS` for rendering heat layers and map interactions.
- CSV parsing & processing (server) — `CsvHelper` for robust CSV handling and any geo libraries needed for coordinate validation and spatial binning.
- Data storage & aggregation — lightweight options such as SQLite or precomputed JSON tile stores; for advanced geospatial queries consider PostGIS.
- Build & tooling — `dotnet` CLI for the server, `Node.js` + `npm`/`yarn` for the Angular client, and optional `Docker`/`Docker Compose` for reproducible local or production deployments.

## Processing and Visualization Components (what I implemented)

- Ingest: I implemented CSV normalization, validation (including coordinate checks), and deduplication routines so the raw sources could be combined reliably.
- Aggregate: I implemented spatial binning and temporal bucketing strategies (configurable to grid, hex, or geohash) to compute tile-level heat intensities.
- API: I created endpoints to return aggregated tiles, raw incidents within a bounding box, and basic metadata/summary endpoints used by the client.
- Client: I built an Angular-based interactive map that requests tile data from the API and renders a heat layer; I added tooltips and sample-incident views for area inspection.

## Design Considerations (summary of decisions I made)

- Privacy: I removed and/or obfuscated personally identifying fields during ingestion to ensure no private data is exposed by the visualizations.
- Performance: I pre-aggregated heavy tiles server-side and designed the API to provide both precomputed tiles and on-demand aggregates. This reduced client rendering load and improved responsiveness at larger zoom levels.
- Data format: I standardized a compact JSON tile format to minimize bandwidth and make client parsing straightforward.
- Extensibility: I implemented flexible query parameters for filtering (e.g., `severity`, `collision_type`, `from`, `to`) so the UI can add filters without server changes.
- Reproducibility: I added configuration and scripts to make ingestion and tile generation repeatable (and suitable for Docker-based runs).

## Running Locally

1. Add your CSV files to the `data/` directory and ensure consistent headers.
2. From the solution root run the server project:

 ```bash
 dotnet run --project CrashDateHeatMap.Server
 ```

3. Open the client or visit the API endpoints to fetch aggregated tiles or raw points.

## Example API ideas

- `GET /api/tiles/{z}/{x}/{y}` — return aggregated tile data for zoom `z` and tile coords `x`,`y`.
- `GET /api/incidents?bbox={minLon},{minLat},{maxLon},{maxLat}` — return raw incidents in a bounding box.
- `GET /api/summary?from=...&to=...` — return aggregated summary statistics.

## Potential Features / Next Steps

- UI filters: fatal vs non-fatal, speeding-involved, pedestrian vs vehicle collisions.
- Temporal animation: play accident intensity over time (hours/days/months).
- Root-cause analysis: correlate collisions with road geometry, lane markings, lighting, or nearby construction.
- Comparative mapping: analyze other Pittsburgh neighborhoods and produce comparative dashboards.

## Use Cases

- Present findings at Town Hall meetings to support infrastructure or policy changes.
- Prioritize road-safety interventions (improved markings, signal timing, redesigns).
- Inform community groups and regional planners where targeted improvements could have the most impact.

## Contributing

- Provide CSV files with consistent headers and a short README describing the source and fields.
- Open issues for bugs, data quality concerns, or feature requests.

## License

Include an appropriate license file in the repository (for example, `LICENSE.txt`).

## Contact / Notes

This project focuses on West Mifflin as a primary study area because of its central location and recent changes to traffic patterns. 
However, the same pipeline can be applied to other Pittsburgh neighborhoods (Oakland, South Side, McKeesport, Pleasant Hills, Upper St. Clair, Glassport, Elizabeth, Jefferson Hills) to compare and diagnose issues across the region.

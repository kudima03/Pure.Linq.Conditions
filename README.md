# Pure.Linq.Conditions

LINQ-compatible `IBool` conditions over `IEnumerable<T>` sequences for the **Pure** ecosystem.

[![.NET build & test](https://github.com/kudima03/Pure.Linq.Conditions/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Linq.Conditions/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.Linq.Conditions/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Linq.Conditions/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.Linq.Conditions)](https://www.nuget.org/packages/Pure.Linq.Conditions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.Linq.Conditions` provides sealed record types that evaluate structural conditions over `IEnumerable<T>` sequences and expose the result as `IBool`. Each condition is a composable value object — lazy, side-effect-free, and fully AOT-compatible.

## Types

| Type | True when |
|------|-----------|
| `EmptyCondition<T>` | The sequence contains no elements |
| `NotEmptyCondition<T>` | The sequence contains at least one element |
| `EqualCondition<T>` | Two sequences contain identical elements in the same order |
| `NotEqualCondition<T>` | Two sequences differ in length or element values |

All types accept `IEnumerable<T>` and implement `IBool` from `Pure.Primitives.Abstractions`.

## Design Principles

- **IBool results** — every condition returns an `IBool`, keeping results composable within the Pure type system.
- **Lazy evaluation** — the sequence is enumerated only when `.BoolValue` is accessed.
- **AOT-compatible** — no reflection; generic constraints are resolved at compile time.

## Dependencies

- [`Pure.Primitives.Abstractions`](https://github.com/kudima03/Pure.Primitives.Abstractions) — `IBool` interface

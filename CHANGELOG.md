# Changelog

All notable changes to Pure.Linq.Conditions are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.0.2.0] — 2026-05-06

- Maintenance release: dependency and build updates.

## [0.1.0-preview.0.1.0] — 2025-11-03

### Added

- **`EmptyCondition<T>`** — `IBool` condition that is `true` when an
  `IEnumerable<T>` sequence contains no elements.
- **`NotEmptyCondition<T>`** — `IBool` condition that is `true` when an
  `IEnumerable<T>` sequence contains at least one element.
- **`EqualCondition<T>`** — `IBool` condition that is `true` when two or
  more `IEnumerable<T>` sequences contain identical elements in the same
  order, using a supplied `Func<T, T, IBool>` equality comparer.
- **`NotEqualCondition<T>`** — `IBool` condition that is `true` when the
  compared `IEnumerable<T>` sequences differ in length or element values.

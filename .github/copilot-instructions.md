# GitHub Copilot Instructions for Area Unlocker (Continued)

## Mod Overview and Purpose
The **Area Unlocker (Continued)** mod is an updated version of Fluffy's original mod with the addition of the reordering fix by lisanhu. This mod removes the arbitrary limit on the number of areas that can be created in the game RimWorld, allowing players to create as many areas as they desire.

## Key Features and Systems
### Unlimited Area Creation
The primary feature of the mod is the removal of the limit on the number of areas players can create. This feature enhances the flexibility and customization options available in RimWorld.

### Area Color Customization
Players can change the colors of areas, providing a personalized touch to their settlements and making it easier to differentiate between various areas.

### XML Integration
The mod integrates with RimWorld's XML files to modify game behavior and add new features. The XML files are located in the `Source/News` directory.

### Harmony Patching
Harmony patching is used to modify existing code in RimWorld to implement the new features. The mod uses the `brrainz.harmony` library to apply patches.

## Coding Patterns and Conventions
### C# Code Structure
The mod is written in C# and follows a structured approach with classes and methods organized in a logical manner. The following are some key classes and methods:

#### Dialog_ColourPicker
This class is responsible for the color picker interface, allowing players to select colors for areas.

#### RecentColours
This class manages the recent colors used by the player, providing a quick way to select previously used colors.

#### TextField
This class is used for text input fields in the mod, providing validation and formatting functions.

#### AreaManager_CanMakeNewAllowed
This class uses Harmony patching to modify the behavior of the `AreaManager` class, allowing players to create new areas without hitting the limit.

### XML Structure
The XML files used by the mod follow the standard RimWorld XML structure. The `About.xml` file contains metadata about the mod, including its dependencies. The `Manifest.xml` file contains the mod's manifest information. The `News` directory contains XML files for update features.

## Harmony Patching
Harmony patching is a powerful tool used to modify existing code in RimWorld. The mod uses Harmony patches to modify the behavior of the `AreaManager` class to allow unlimited area creation.

### Example Patch
csharp
using HarmonyLib;

namespace AreaUnlocker
{
    [HarmonyPatch(typeof(AreaManager), "CanMakeNewAllowed")]
    public static class AreaManager_CanMakeNewAllowed
    {
        public static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}


## Suggestions for Copilot
### C# Code
- When working with the color picker interface, use the `Dialog_ColourPicker` class to create and manage the color picker.
- When working with recent colors, use the `RecentColours` class to manage the list of recent colors.
- When working with text input fields, use the `TextField` class to validate and format input.

### XML Integration
- When modifying game behavior using XML, follow the standard RimWorld XML structure.
- When adding new features, use the `News` directory to create XML files for update features.

### Harmony Patching
- When modifying existing code using Harmony, use the `HarmonyPatch` attribute to specify the target method.
- When writing patch methods, use the `Prefix`, `Postfix`, or `Transpiler` methods to modify the behavior of the target method.

## Conclusion
The **Area Unlocker (Continued)** mod is a powerful tool that enhances the flexibility and customization options available in RimWorld. By removing the limit on the number of areas that can be created and allowing players to customize area colors, the mod provides a more personalized and efficient experience. If you have any questions or need further assistance, please use the Discord-channel for error-reporting or post your log using the Log Uploader.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.


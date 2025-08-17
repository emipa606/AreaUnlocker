# GitHub Copilot Instructions for RimWorld Mod: Area Unlocker (Continued)

## Mod Overview and Purpose

**Area Unlocker (Continued)** is an updated version of Fluffy's original mod for RimWorld. The primary purpose of this mod is to remove the arbitrary limit on the number of areas a player can create within the game, allowing for greater flexibility and customization. Additionally, the mod allows users to change area colors. However, note that UI components related to area selection were originally designed for only five areas per type, and creating too many areas may cause UI issues, depending on screen resolution and UI scale.

## Key Features and Systems

- **Unlimited Areas:** Removes any cap on the number of areas you can create, enhancing gameplay customization.
- **Color Customization:** Allows users to change the colors of areas to better organize and categorize game spaces.
- **Recent Colours Tracking:** Maintains a list of recently used colors for quick access.
- **Harmony Patching:** Integrates with Harmony to ensure compatibility with other mods and make necessary modifications to the game's behavior.

## Coding Patterns and Conventions

- **Use of Public and Private Methods:** Classes include both public methods for interaction with other parts of the game and private methods for internal logic management.
- **Descriptive Method Names:** All methods have descriptive names, such as `CreateAlphaPickerBG` and `NotifyHexUpdated`, to clearly indicate their functionality.
- **Compact and Purposeful Classes:** Each class is designed with a specific purpose, such as `Dialog_ColourPicker` for handling color selection dialogues and `RecentColours` for managing recently used colors.

## XML Integration

While this mod primarily involves C# coding, XML might be used in the context of RimWorld mods for managing settings or defining static data. This is not explicitly detailed in the provided files, but ensure to follow RimWorld's standard XML structure if applicable.

## Harmony Patching

Harmony is used in `HarmonyPatch_Dialog_ManageAreas` to apply patches smoothly without disrupting the game's default functions or interacting with other mods. All patches should maintain logic encapsulation and ensure changes are reversible where possible. In this project, Harmony allows modifications while ensuring compatibility, although not all UI limitations are addressed.

## Suggestions for Copilot

Here are some suggestions to guide Copilot in generating code to improve this mod:

- **Extend UI Compatibility:** Suggest potential methods or classes that could be extended to adapt the UI for handling more than five areas visually.
- **Harmony Hooks:** Provide ideal hooks for inserting new logic without disrupting existing game or mod functionality.
- **Color Management Enhancements:** Propose efficient ways to handle color pickers and storage, minimizing performance impact.
- **Error Handling:** Integrate thorough error-checking and user feedback for scenarios where area reordering may fail or UI limitations are reached.

### Additional Notes

- **Licensing Considerations:** Keep the MIT license and CC-BY-SA 4.0 license in mind when expanding or modifying the code.
- **Ethical Responsibility:** Remember to support and respect the communities involved, like the mention of Ukraine, though this does not affect the coding but emphasizes the broader responsibility of developers.

The mod is designed to enhance the gameplay experience of RimWorld players by allowing greater control over area management while maintaining compatibility with existing mod ecosystems.

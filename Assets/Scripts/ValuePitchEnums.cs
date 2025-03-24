/*
 * Name: Jack Gu
 * Date: 3/21/25
 * Desc: Defines enums for Value and Pitch of Notes
 */

namespace ValuePitchEnums
{
    // Convert to int for the number of quarter note lengths
    public enum Value
    {
        Quarter = 1, Half = 2
    }

    public enum Pitch
    {
        Rest, C4, D4, E4, F4, G4, A4, B4, C5
    }
}

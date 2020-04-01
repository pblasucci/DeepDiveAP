module colorspace.Before

open System.Drawing

let getHSV (c:Color) =
  // Extract R,G, and B components and normalize to [0.0 ... 1.0]
  let r, g, b = (float c.R / 255.0, float c.G / 255.0, float c.B / 255.0)
  // Calculate "base" components
  let value = max g (max b r)
  let chroma = value - (min g (min b r))
  // Determine hue and saturation
  let hue,saturation =
    if chroma = 0.0 then
      // Color is achromatic (no hue or saturation)
      (0.0, 0.0)
    else
      // hue is based on which RGB component contributes the "value"
      let primary =
        if r >= g && r >= b then
          (g - b) / chroma
        elif g >= r && g >= b then
          ((b - r) / chroma) + 2.0
        else
          ((r - g) / chroma) + 4.0
      let hue' = (60.0 * primary)
      // saturation is simply the ratio of chroma to value
      let sat = chroma / value
      (hue', sat)
  // return HSV
  (hue, saturation, value)

let inline toPercent number =
  // some real numbers ought to be percentages
  100.0 * float number

let printColor = sprintf "%s: %3.2f, %3.2f, %3.2f"

let getColorSpaces (color:Color) =
  let hue1  = color.GetHue()
  let sat1  = color.GetSaturation()
  let light = color.GetBrightness()

  let hue2, sat2, value = getHSV color

  let colorSpaces = {|
    Name = color.Name
    RGB = printColor "RGB" (float color.R) (float color.G) (float color.B)
    HSL = printColor "HSL" (float hue1) (toPercent sat1) (toPercent light)
    HSV = printColor "HSV" (float hue2) (toPercent sat2) (toPercent value)
  |}
  colorSpaces

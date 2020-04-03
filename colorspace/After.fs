module colorspace.After

open System.Drawing

let (|RGB|) (color:Color) =
  RGB (float color.R, float color.G, float color.B)

let (|HSL|) (color:Color) =
  //NOTE: MSDN lists this as HSB, but the math on Wikipedia argues in favor of HSL
  let hue = color.GetHue() |> float
  let saturation = color.GetSaturation() |> float
  let lightness = color.GetBrightness() |> float
  HSL (hue, saturation, lightness)

let (|ValueIsR|ValueIsG|ValueIsB|) (RGB (r, g, b)) =
  // Normalize R,G, and B components to [0.0 ... 1.0]
  let r', g', b' = (r / 255.0, g / 255.0, b / 255.0)
  // hue is based on which RGB component contributes the "value"
  if   r >= g && r >= b then ValueIsR (r', g', b')
  elif g >= r && g >= b then ValueIsG (r', g', b')
  else ValueIsB (r', g', b')

let (|HSV|) (HSL (_, _, l) as color) =
  //NOTE: Adapted from http://en.wikipedia.org/wiki/HSL_and_HSV

  // Calculate "base" components
  let value, shift, step =
    match color with
    | ValueIsR (value, g, b) -> (value, g - b, 0.0)
    | ValueIsG (r, value, b) -> (value, b - r, 2.0)
    | ValueIsB (r, g, value) -> (value, r - g, 4.0)

  // Compute the value's range
  let chroma = abs (2.0 * (value - l)) // := value - MIN(R, G, B) when R, G, B ∈ [0, 1]

  // Determine hue and saturation
  let hue, saturation =
    if chroma = 0.0 then
      // Value is achromatic
      (0.0, 0.0)
    else
      let h = 60.0 * (step + (shift / chroma))
      let s = chroma / value
      (h, s)

  HSV (hue, saturation, value)

let getColorSpaces (RGB (r, g, b) & HSL (c, t, l) & HSV (h, s, v) as color) =
  // some real numbers ought to be percentages
  let t, l, s, v = (t * 100.0, l * 100.0, s * 100.0, v * 100.0)
  let colorSpaces = {|
    Name = color.Name
    RGB = sprintf "RGB: %3.2f, %3.2f, %3.2f" r g b
    HSL = sprintf "HSL: %3.2f, %3.2f, %3.2f" c t l
    HSV = sprintf "HSV: %3.2f, %3.2f, %3.2f" h s v
  |}
  colorSpaces

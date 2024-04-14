using ImGuiNET;
using System;
using System.Numerics;

namespace HydraNet
{
    public class HydraGui
    {
        public static void SliderFloat(string label, ref float value, float min, float max)
        {
            ImGui.Text(label);
            ImGui.SameLine(ImGui.GetContentRegionAvail().X * 0.85f);
            ImGui.Text(((int)value).ToString());

            var HydraSlider = ImGuiNET.ImGui.GetStyle();

            HydraSlider.FramePadding = new Vector2(0f, 2f); // Set frame padding to 0 for slim track line
            HydraSlider.FrameRounding = 5.0f;
            HydraSlider.FrameBorderSize = 1.0f;
            HydraSlider.ScrollbarSize = 20.0f;
            HydraSlider.GrabMinSize = 20.0f;
            HydraSlider.ScrollbarRounding = 20.0f;
            HydraSlider.GrabRounding = 20.0f;

            ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X * 0.95f);
            ImGui.SliderFloat("##" + label, ref value, min, max, string.Empty);
            ImGui.PopItemWidth();
        }

        public static void SliderFloat2(string label, ref float value , float min, float max)
        {
            ImGui.Text(label);
            ImGui.SameLine(ImGui.GetContentRegionAvail().X * 0.85f);
            ImGui.Text(((int)value).ToString());

            var HydraSlider = ImGuiNET.ImGui.GetStyle();

            HydraSlider.FramePadding = new Vector2(0f, 2f); // Set frame padding to 0 for slim track line
            HydraSlider.FrameRounding = 5.0f;
            HydraSlider.FrameBorderSize = 1.0f;
            HydraSlider.ScrollbarSize = 20.0f;
            HydraSlider.GrabMinSize = 20.0f;
            HydraSlider.ScrollbarRounding = 20.0f;
            HydraSlider.GrabRounding = 20.0f;

            ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X * 0.95f);
            ImGui.SliderFloat("##" + label, ref value, min, max, string.Empty);
            ImGui.PopItemWidth();
        }

        public static void TextDisabled(string text)
        {
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.5f, 0.5f, 0.5f, 1.0f)); // Adjust color values as desired

            ImGui.Text(text);

            ImGui.PopStyleColor();
        }

        public static bool Switch(string label, ref bool value)
        {
            ImGui.PushID(label);

            // Calculate the height of the label text
            float labelHeight = ImGui.GetTextLineHeight();

            // Calculate the height of the switch
            float switchHeight = 25f;

            // Calculate the vertical offset needed to center the switch with respect to the label
            float yOffset = (labelHeight - switchHeight) / 2f;

            // Calculate the total height of each line
            float totalHeight = Math.Max(labelHeight, switchHeight);

            // Move to the same line as the label
            ImGui.AlignTextToFramePadding(); // Align label to the start
            ImGui.Text(label);

            // Add spacing between label and switch
            float labelSpacing = 175f; // Adjust spacing between label and switch
            ImGui.SameLine(labelSpacing); // Align the switch to the right

            // Calculate vertical offset for the switch
            float switchYOffset = (totalHeight - switchHeight) / 2f;

            // Add vertical offset
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + switchYOffset);

            float width = 50f;
            float height = 25f; // Adjust the height to match other switches
            float padding = 1f;
            float radius = height / 2f;
            float innerRadius = radius - padding;

            Vector2 size = new Vector2(width, height);
            bool hovered = ImGui.IsMouseHoveringRect(ImGui.GetCursorScreenPos(), ImGui.GetCursorScreenPos() + size);
            if (hovered)
                ImGui.SetMouseCursor(ImGuiMouseCursor.Hand);

            bool pressed = false;
            if (hovered && ImGui.IsMouseClicked(ImGuiMouseButton.Left))
            {
                value = !value;
                pressed = true;
            }

            ImDrawListPtr drawList = ImGui.GetWindowDrawList();
            Vector2 startPos = ImGui.GetCursorScreenPos();
            Vector2 endPos = startPos + size;

            float t = value ? 1f : 0f;

            // Modify background color based on switch value
            var bgColor = value ? new Vector4(1.0f, 0.0f, 0.0f, 1.0f) : new Vector4(0.1f, 0.1f, 0.1f, 1f);

            // Draw the switch background
            drawList.AddRectFilled(startPos, endPos, ImGui.GetColorU32(bgColor), radius);

            ImGui.PopID();

            // Draw the knob
            var baseColor = new Vector4(0.5f, 0.5f, 0.5f, 1f);
            var knobColor = new Vector4(0.8f, 0.8f, 0.8f, 1f);

            float pos = startPos.X + padding + (endPos.X - innerRadius * 2 - padding - (startPos.X + padding)) * t;
            float knobYPos = startPos.Y + radius + switchYOffset; // Adjusted to center vertically with label
            drawList.AddCircleFilled(new Vector2(pos + innerRadius, knobYPos), innerRadius, ImGui.GetColorU32(baseColor));
            drawList.AddCircleFilled(new Vector2(pos + innerRadius, knobYPos), innerRadius - padding, ImGui.GetColorU32(knobColor));

            // Move cursor to the next line
            ImGui.NewLine(); // Start a new line
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + ImGui.GetStyle().ItemSpacing.Y); // Add spacing between switches

            return pressed;
        }
    }
}
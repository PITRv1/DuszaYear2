shader_type spatial;
render_mode unshaded, fog_disabled, depth_test_disabled;

uniform vec2 resolution = vec2(720.0, 360.0);

uniform vec4 outline_color : source_color = vec4(0.0, 0.0, 0.0, 1.0);
uniform int outline_width : hint_range(-4, 10, 1) = 1;
uniform float outline_draw_distance = 50.0;

uniform sampler2D screen_tex : hint_screen_texture, filter_linear;
uniform sampler2D depth_tex : hint_depth_texture, filter_linear;


void vertex() {
	POSITION = vec4(VERTEX.xy, 1.0, 1.0);
}

void fragment() {
	vec2 uv = floor(SCREEN_UV * resolution) / resolution;

	vec2 screen_size = vec2(textureSize(screen_tex, 1) / outline_width);

	float px = 0.5/screen_size.x;
	float py = 0.5/screen_size.y;

	float d = texture(depth_tex, uv ).x;
	float du = texture(depth_tex, uv+vec2(0.0, py)).x;
	float dd = texture(depth_tex, uv+vec2(0.0, -py)).x;
	float dr = texture(depth_tex, uv+vec2(px, 0.0)).x;
	float dl = texture(depth_tex, uv+vec2(-px, 0.0)).x;


	float dq = texture(depth_tex, uv+vec2(-px, py)).x;
	float de = texture(depth_tex, uv+vec2(px, py)).x;
	float dz = texture(depth_tex, uv+vec2(-px, -py)).x;
	float dc = texture(depth_tex, uv+vec2(px, -py)).x;

	float edge = 0.0 + abs(abs(abs(d)-abs(du)) - abs(abs(d)-abs(dd)))
           + abs(abs(abs(d)-abs(dl)) - abs(abs(d)-abs(dr)))
           + abs(abs(abs(d)-abs(dq)) - abs(abs(d)-abs(dc)))
           + abs(abs(abs(d)-abs(dz)) - abs(abs(d)-abs(de)));

	float z_near = 0.05; // Match your camera's near plane
	float z_far = 4000.0; // Match your camera's far plane
	float linear_depth = (2.0 * z_near * z_far) / (z_far + z_near - d * (z_far - z_near));

	//float distance_fade = 1.0 - clamp(linear_depth / outline_draw_distance, 0.0, 1.0);

	float distance_factor = clamp(linear_depth / 100.0, 0.0, 1.0);

	//float distance_fade = 1.0 - (distance_factor * distance_factor); // Cubic fade

	float distance_fade = outline_draw_distance; // Cubic fade


	ALPHA = edge * distance_fade;
	ALPHA = clamp(ALPHA, 0.0, 1.0);
	ALBEDO = outline_color.rgb;
}
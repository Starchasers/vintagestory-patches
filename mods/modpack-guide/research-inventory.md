# Toolsmith 1.2.19 static inventory and progression research

Archive: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/toolsmith_1.2.19.zip`
SHA-256: `325ead64a9192ea7426d017e1c5141286f860f95c3af5dd34b79ad14f060974b`

Static JSON assets/configs only; DLL never executed. Conditional folders are candidate integrations, not proof of runtime loader behavior. Installed ZIP metadata is not proof a mod is enabled.

## Native collectible codes and exact expansion

Only itemtypes/blocktypes create native collectible codes. Shape files, language names, and tool classification records are NOT new item IDs. Attribute wood/color/treatment/grip states are stack attributes, not hyphenated code variants.

### toolsmith:grindstone (block)
Source: `assets/toolsmith/blocktypes/grindstone.json`
Code variants (16): `toolsmith:grindstone-granite-north`, `toolsmith:grindstone-granite-east`, `toolsmith:grindstone-granite-south`, `toolsmith:grindstone-granite-west`, `toolsmith:grindstone-sandstone-north`, `toolsmith:grindstone-sandstone-east`, `toolsmith:grindstone-sandstone-south`, `toolsmith:grindstone-sandstone-west`, `toolsmith:grindstone-shale-north`, `toolsmith:grindstone-shale-east`, `toolsmith:grindstone-shale-south`, `toolsmith:grindstone-shale-west`, `toolsmith:grindstone-andesite-north`, `toolsmith:grindstone-andesite-east`, `toolsmith:grindstone-andesite-south`, `toolsmith:grindstone-andesite-west`
Handbook grouping: `{"groupBy": ["grindstone-*"]}`
Tags: `[]`

### toolsmith:workbench (block)
Source: `assets/toolsmith/blocktypes/workbench.json`
Code variants (4): `toolsmith:workbench-north`, `toolsmith:workbench-east`, `toolsmith:workbench-south`, `toolsmith:workbench-west`
Handbook grouping: `{}`
Tags: `[]`

### toolsmith:fabricstrips (item)
Source: `assets/toolsmith/itemtypes/fabricstrips.json`
Code variants (5): `toolsmith:fabricstrips-rawhide`, `toolsmith:fabricstrips-cloth`, `toolsmith:fabricstrips-leather`, `toolsmith:fabricstrips-sturdy`, `toolsmith:fabricstrips-wool`
Handbook grouping: `{"groupByByType": {"*": ["fabricstrips-{material}"]}}`
Tags: `["toolsmith-part", "toolsmith-binding"]`
Attribute texture `material`, pattern `*-cloth`, 17 values: `game:block/cloth/linen/plain`, `game:block/cloth/linen/mordant`, `game:block/cloth/linen/blue`, `game:block/cloth/linen/red`, `game:block/cloth/linen/yellow`, `game:block/cloth/linen/green`, `game:block/cloth/linen/purple`, `game:block/cloth/linen/pink`, `game:block/cloth/linen/orange`, `game:block/cloth/linen/brown`, `game:block/cloth/linen/gray`, `game:block/cloth/linen/black`, `game:block/cloth/linen/white`, `tailorsdelight:block/cloth/dark/darkblue`, `tailorsdelight:block/cloth/dark/darkbrown`, `tailorsdelight:block/cloth/dark/darkgreen`, `tailorsdelight:block/cloth/dark/darkred`
Attribute texture `material`, pattern `*-leather`, 11 values: `game:block/leather/plain`, `game:block/leather/orange`, `game:block/leather/black`, `game:block/leather/red`, `game:block/leather/blue`, `game:block/leather/purple`, `game:block/leather/pink`, `game:block/leather/white`, `game:block/leather/yellow`, `game:block/leather/gray`, `game:block/leather/green`
Attribute texture `material`, pattern `*-wool`, 17 values: `wool:block/cloth/wool/plain`, `game:block/cloth/linen/mordant`, `wool:block/cloth/wool/blue`, `wool:block/cloth/wool/red`, `wool:block/cloth/wool/yellow`, `wool:block/cloth/wool/green`, `wool:block/cloth/wool/purple`, `wool:block/cloth/wool/pink`, `wool:block/cloth/wool/orange`, `wool:block/cloth/wool/brown`, `wool:block/cloth/wool/gray`, `wool:block/cloth/wool/black`, `wool:block/cloth/wool/white`, `wool:block/cloth/wool/darkblue`, `wool:block/cloth/wool/darkbrown`, `wool:block/cloth/wool/darkgreen`, `wool:block/cloth/wool/darkred`

### toolsmith:sandpaper (item)
Source: `assets/toolsmith/itemtypes/sandpaper.json`
Code variants (1): `toolsmith:sandpaper`
Handbook grouping: `{}`
Tags: `["toolsmith-maintenance"]`

### toolsmith:tinkertoolparts (item)
Source: `assets/toolsmith/itemtypes/tinkertoolparts.json`
Code variants (1): `toolsmith:tinkertoolparts`
Handbook grouping: `{}`
Tags: `[]`

### toolsmith:whetstone (item)
Source: `assets/toolsmith/itemtypes/whetstone.json`
Code variants (5): `toolsmith:whetstone-chert`, `toolsmith:whetstone-granite`, `toolsmith:whetstone-andesite`, `toolsmith:whetstone-basalt`, `toolsmith:whetstone-peridotite`
Handbook grouping: `{"groupBy": ["whetstone-*"]}`
Tags: `["toolsmith-maintenance"]`

### toolsmith:carpentedhandle (item)
Source: `assets/toolsmith/itemtypes/handles/carpentedhandle.json`
Code variants (1): `toolsmith:carpentedhandle`
Handbook grouping: `{"groupBy": ["carpentedhandle"]}`
Tags: `["toolsmith-part", "toolsmith-handle"]`
Attribute texture `wood`, pattern `*`, 13 values: `toolsmith:block/tools/toolaged`, `toolsmith:block/tools/toolbirch`, `toolsmith:block/tools/tooloak`, `toolsmith:block/tools/toolmaple`, `toolsmith:block/tools/toolpine`, `toolsmith:block/tools/toolacacia`, `toolsmith:block/tools/toolkapok`, `toolsmith:block/tools/toolbaldcypress`, `toolsmith:block/tools/toollarch`, `toolsmith:block/tools/toolredwood`, `toolsmith:block/tools/toolebony`, `toolsmith:block/tools/toolwalnut`, `toolsmith:block/tools/toolpurpleheart`

### toolsmith:crudehandle (item)
Source: `assets/toolsmith/itemtypes/handles/crudehandle.json`
Code variants (1): `toolsmith:crudehandle`
Handbook grouping: `{}`
Tags: `["toolsmith-part", "toolsmith-handle"]`

### toolsmith:handle (item)
Source: `assets/toolsmith/itemtypes/handles/handle.json`
Code variants (1): `toolsmith:handle`
Handbook grouping: `{"groupBy": ["handle"]}`
Tags: `["toolsmith-part", "toolsmith-handle"]`
Attribute texture `wood`, pattern `*`, 13 values: `toolsmith:block/tools/toolaged`, `toolsmith:block/tools/toolbirch`, `toolsmith:block/tools/tooloak`, `toolsmith:block/tools/toolmaple`, `toolsmith:block/tools/toolpine`, `toolsmith:block/tools/toolacacia`, `toolsmith:block/tools/toolkapok`, `toolsmith:block/tools/toolbaldcypress`, `toolsmith:block/tools/toollarch`, `toolsmith:block/tools/toolredwood`, `toolsmith:block/tools/toolebony`, `toolsmith:block/tools/toolwalnut`, `toolsmith:block/tools/toolpurpleheart`

## Counts

Baseline native codes: 15 items, 20 orientation-expanded blocks; 9 source definitions. Blocks include four workbench orientations and four grindstone stones times four orientations. Creative entries expose only north-facing station blocks. `tinkertoolparts` is a hidden intermediate, not a final tool.

## Progression and configuration caveats

- Starter: stick/bone handles and grass/reeds bindings; knife + firewood -> crudehandle. Knife + supportbeam -> handle (Carved Tool Handle); knife + supportbeam + sandpaper -> carpentedhandle (Professional Tool Handle). Exact recipes below.
- Treatments/grips are optional handle attributes: stick cannot have either; bone/crude can have grip but not treatment; carved/professional accept both. Professional drying multiplier 2 versus 1 for others. Source `assets/toolsmith/config/toolsmith/parts/handles/handles-vanilla.json`.
- Bindings progress through reeds, twine/rawhide/rope/cloth/leather/sturdy leather/glue and metal nails/parts. They are not a simple enforced tech tree. Exact records and stats below.
- Knapped whetstones provide early portable honing (durability 300). Grindstone is a permanent workshop block requiring rock and planks; workbench needs planks and saw, supports assembly/deconstruction/reforge preparation per bundled handbook.
- Actual server config has `EnableGridRecipesForToolCrafting=false`, so do not teach generated grid TOOL ASSEMBLY as currently enabled. Component grid recipes are separate assets. `HeadDurabilityMult=5`, `SharpnessMult=1.5`, `SharpWear=0.15`, `BluntWear=0.02`, `UseBitsForSmithing=true`.
- `ToolsmithPartsStats.json` has `EnableEdits=false`: persisted values are configuration evidence, not independently proven overrides. `EnableEditsForRegex=false` likewise means regex lists are not a verified editable override. Persisted regex differs from archive: includes barkspud/pitsaw/maul and custom blacklist sentinel `__armoryfork_toolsmith_allow__`; do not discard or silently repair it.
- Handbook says Drawbacks are not implemented in basics but elsewhere describes them as guaranteed. Treat that as inconsistent bundled documentation, not verified runtime behavior.
- `treatments-vanilla.json` actually includes oilportion-flax and oilportion-olive; handbook claim oil only via cross-compatibility is stale.
- Copper cold-working patch also sets tin/lead metalplate workable temperature to zero; see `assets/toolsmith/patches/coldforgemetals.json`. Supportbeam output patch sets 4, with an indappledgroves-dependent disabling patch under `assets/toolsmith/patches/compatability/in-dappled-groves-disable-supportbeam-patch.json`.

## Conditional compatibility folders

### ancienttools: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/ancienttools/patches/fabricstrips-additions.json`
```json
[
  {
    "op": "addmerge",
    "path": "/variantgroups/0/states/-",
    "file": "toolsmith:itemtypes/fabricstrips.json",
    "value": "atleather"
  },
  {
    "op": "addmerge",
    "path": "/behaviorsByType/*-atleather",
    "file": "toolsmith:itemtypes/fabricstrips.json",
    "value": [
      {
        "name": "toolsmith:ModularPartRenderingFromAttributes",
        "properties": {
          "textures": [
            {
              "code": "material",
              "Default": "wool:block/cloth/wool/plain",
              "values": [
                "ancienttools:item/resource/leather/acacia",
                "ancienttools:item/resource/leather/aged",
                "ancienttools:item/resource/leather/baldcypress",
                "ancienttools:item/resource/leather/birch",
                "ancienttools:item/resource/leather/cypress",
                "ancienttools:item/resource/leather/ebony",
                "ancienttools:item/resource/leather/kapok",
                "ancienttools:item/resource/leather/larch",
                "ancienttools:item/resource/leather/maple",
                "ancienttools:item/resource/leather/pine",
                "ancienttools:item/resource/leather/purpleheart",
                "ancienttools:item/resource/leather/redwood",
                "ancienttools:item/resource/leather/walnut",
                "ancienttools:item/resource/buckskin"
              ]
            }
          ],
          "skipCreativeInventoryAdditions": false,
          "creativeTabs": [
            "general",
            "items"
          ]
        }
      }
    ]
  }
]
```
`assets/toolsmith/compatibility/ancienttools/recipes/grid/fabricstrips-atleather-cloth.json`
```json
[
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-acacia"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/acacia"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-aged"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/aged"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-baldcypress"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/baldcypress"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-birch"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/birch"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-cypress"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/cypress"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-ebony"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/ebony"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-kapok"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/kapok"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-larch"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/larch"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-maple"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/maple"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-pine"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/pine"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-purpleheart"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/purpleheart"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-redwood"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/redwood"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-walnut"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/walnut"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:buckskin"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/buckskin"
          }
        }
      }
    }
  }
]
```
### botaniastory: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-axe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(elementium)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.4
      },
      "scale": 3.2
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.43
      },
      "scale": 3.55
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-hammer.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/simple"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-hoe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(elementium)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -105
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.33
      },
      "scale": 2.9
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -125
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.38
      },
      "scale": 3.76
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-knife.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-pickaxe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-elementium",
    "value": {
      "material": {
        "base": "game:item/resource/plate/elementium"
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/texturesByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*",
    "value": {
      "material": {
        "base": "block/metal/ingot/{metal}"
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-prospectingpick.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/simple"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-saw.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/simple"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-scythe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/elementium/vanilla-shovel.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-elementium",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/@shovelhead-(elementium)",
    "value": {
      "translation": {
        "x": -0.2,
        "y": -0.4,
        "z": -0.15
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/tpHandTransformByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.4,
        "z": -0.1
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/@shovel-(elementium)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/attributes/groundStorageTransformByType/*",
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-axe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(manasteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.4
      },
      "scale": 3.2
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.43
      },
      "scale": 3.55
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-hammer.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/simple"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-hoe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(manasteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -105
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.33
      },
      "scale": 2.9
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -125
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.38
      },
      "scale": 3.76
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-knife.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-pickaxe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-manasteel",
    "value": {
      "material": {
        "base": "game:item/resource/plate/manasteel"
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/texturesByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*",
    "value": {
      "material": {
        "base": "block/metal/ingot/{metal}"
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-prospectingpick.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/simple"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-saw.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/simple"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-scythe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/manasteel/vanilla-shovel.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-manasteel",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/@shovelhead-(manasteel)",
    "value": {
      "translation": {
        "x": -0.2,
        "y": -0.4,
        "z": -0.15
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/tpHandTransformByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.4,
        "z": -0.1
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/@shovel-(manasteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/attributes/groundStorageTransformByType/*",
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-axe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(terrasteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.4
      },
      "scale": 3.2
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.43
      },
      "scale": 3.55
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-hammer.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/simple"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-hoe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(terrasteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -105
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.33
      },
      "scale": 2.9
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -125
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.38
      },
      "scale": 3.76
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-knife.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-pickaxe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-terrasteel",
    "value": {
      "material": {
        "base": "game:item/resource/plate/terrasteel"
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/texturesByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*",
    "value": {
      "material": {
        "base": "block/metal/ingot/{metal}"
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-prospectingpick.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/simple"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-saw.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/simple"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-scythe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/botaniastory/patches/partshapes/terrasteel/vanilla-shovel.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-terrasteel",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/@shovelhead-(terrasteel)",
    "value": {
      "translation": {
        "x": -0.2,
        "y": -0.4,
        "z": -0.15
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/tpHandTransformByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.4,
        "z": -0.1
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/@shovel-(terrasteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/attributes/groundStorageTransformByType/*",
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### butchering: exact installed ZIP mod ID present = True
`assets/toolsmith/compatibility/butchering/recipes/grid/sandpaper-from-glues.json`
```json
[
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:woodbucket"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-sinew-cold"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:bowl-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-sinew-cold"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:jug-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-sinew-cold"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:woodbucket"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-hide-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:bowl-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-hide-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:jug-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-hide-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  }
]
```
### floralzonescaperegion: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/floralzonescaperegion/patches/add-woods-to-handles.json`
```json
[
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/handle.json",
    "value": [
      "floralzonescaperegion:block/wood/debarked/afrocarpusfalcatus",
      "floralzonescaperegion:block/wood/debarked/ocoteabullata",
      "floralzonescaperegion:block/wood/debarked/searsialancea"
    ]
  },
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/carpentedhandle.json",
    "value": [
      "floralzonescaperegion:block/wood/debarked/afrocarpusfalcatus",
      "floralzonescaperegion:block/wood/debarked/ocoteabullata",
      "floralzonescaperegion:block/wood/debarked/searsialancea"
    ]
  }
]
```
`assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonescaperegion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonescaperegion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### floralzonescaribbeanregion: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/floralzonescaribbeanregion/patches/add-woods-to-handles.json`
```json
[
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/handle.json",
    "value": [
      "floralzonescaribbeanregion:block/wood/debarked/lysilomalatisiliquum"
    ]
  },
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/carpentedhandle.json",
    "value": [
      "floralzonescaribbeanregion:block/wood/debarked/lysilomalatisiliquum"
    ]
  }
]
```
`assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonescaribbeanregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonescaribbeanregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### floralzonescentralaustralianregion: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/floralzonescentralaustralianregion/patches/add-woods-to-handles.json`
```json
[
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/handle.json",
    "value": [
      "floralzonescentralaustralianregion:block/wood/debarked/corymbiaaparrerinja",
      "floralzonescentralaustralianregion:block/wood/debarked/eucalyptuscamaldulensis"
    ]
  },
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/carpentedhandle.json",
    "value": [
      "floralzonescentralaustralianregion:block/wood/debarked/corymbiaaparrerinja",
      "floralzonescentralaustralianregion:block/wood/debarked/eucalyptuscamaldulensis"
    ]
  }
]
```
`assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonescentralaustralianregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonescentralaustralianregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### floralzonesmediterraneanregion: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/floralzonesmediterraneanregion/patches/add-woods-to-handles.json`
```json
[
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/handle.json",
    "value": [
      "floralzonesmediterraneanregion:block/wood/debarked/populusalba",
      "floralzonesmediterraneanregion:block/wood/debarked/taxusbaccata"
    ]
  },
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/carpentedhandle.json",
    "value": [
      "floralzonesmediterraneanregion:block/wood/debarked/populusalba",
      "floralzonesmediterraneanregion:block/wood/debarked/taxusbaccata"
    ]
  }
]
```
`assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonesmediterraneanregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonesmediterraneanregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### floralzonesneozeylandicregion: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/floralzonesneozeylandicregion/patches/add-woods-to-handles.json`
```json
[
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/handle.json",
    "value": [
      "floralzonesneozeylandicregion:block/wood/debarked/araucariaheterophylla",
      "floralzonesneozeylandicregion:block/wood/debarked/dacrydiumcupressinum",
      "floralzonesneozeylandicregion:block/wood/debarked/nothofagusmenziesii",
      "floralzonesneozeylandicregion:block/wood/debarked/podocarpustotara"
    ]
  },
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/carpentedhandle.json",
    "value": [
      "floralzonesneozeylandicregion:block/wood/debarked/araucariaheterophylla",
      "floralzonesneozeylandicregion:block/wood/debarked/dacrydiumcupressinum",
      "floralzonesneozeylandicregion:block/wood/debarked/nothofagusmenziesii",
      "floralzonesneozeylandicregion:block/wood/debarked/podocarpustotara"
    ]
  }
]
```
`assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonesneozeylandicregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonesneozeylandicregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### geoaddons: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/geoaddons/patches/grindstone-additions.json`
```json
[
  {
    "op": "addeach",
    "path": "/variantgroups/0/states/-",
    "file": "toolsmith:blocktypes/grindstone.json",
    "value": [
      "diorite",
      "gabbro",
      "quartzite"
    ]
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-diorite-*",
    "file": "toolsmith:blocktypes/grindstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/diorite-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-gabbro-*",
    "file": "toolsmith:blocktypes/grindstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/gabbro-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-quartzite-*",
    "file": "toolsmith:blocktypes/grindstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/quartzite-inside"
      }
    }
  }
]
```
`assets/toolsmith/compatibility/geoaddons/patches/whetstone-additions.json`
```json
[
  {
    "op": "addeach",
    "path": "/variantgroups/0/states/-",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": [
      "diorite",
      "gabbro",
      "quartzite",
      "jade",
      "jasper"
    ]
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-diorite",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/diorite-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-gabbro",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/gabbro-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-quartzite",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/quartzite-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-jade",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/jade-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-jasper",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/jasper-inside"
      }
    }
  }
]
```
`assets/toolsmith/compatibility/geoaddons/recipes/grid/grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-diorite"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-diorite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-gabbro"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-gabbro-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-quartzite"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-quartzite-north",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/geoaddons/recipes/knapping/whetstone.json`
```json
[
  {
    "ingredient": {
      "type": "item",
      "code": "game:stone-*",
      "name": "rock",
      "allowedVariants": [
        "diorite",
        "gabbro",
        "quartzite",
        "jade",
        "jasper"
      ]
    },
    "pattern": [
      [
        "__________",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "__________"
      ]
    ],
    "name": "Stone whetstone",
    "output": {
      "type": "item",
      "code": "toolsmith:whetstone-{rock}"
    }
  }
]
```
### meteoricsteel: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-axe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(meteoricsteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.4
      },
      "scale": 3.2
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.43
      },
      "scale": 3.55
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-hammer.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/simple"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-hoe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(meteoricsteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -105
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.33
      },
      "scale": 2.9
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -125
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.38
      },
      "scale": 3.76
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-knife.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-pickaxe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-prospectingpick.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/simple"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-saw.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/simple"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-scythe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/patches/partshapes/vanilla-shovel.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-meteoricsteel",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/@shovelhead-(meteoricsteel)",
    "value": {
      "translation": {
        "x": -0.2,
        "y": -0.4,
        "z": -0.15
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/tpHandTransformByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.4,
        "z": -0.1
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/@shovel-(meteoricsteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/attributes/groundStorageTransformByType/*",
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/config/toolsmith/parts/bindings/bindings-meteoricsteel.json`
```json
[
  {
    "id": "metalnailsandstrips-meteoricsteel",
    "bindingStatTag": "meteoricsteelnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  }
]
```
`assets/toolsmith/compatibility/meteoricsteel/config/toolsmith/stats/bindings/bindings-meteoricsteel.json`
```json
[
  {
    "id": "meteoricsteelnails",
    "texturePath": "game:block/metal/plate/meteoricsteel",
    "langTag": "binding-nails-meteoricsteel",
    "baseHPfactor": 2.3,
    "selfHPBonus": 0.65,
    "handleHPBonus": 0.45,
    "recoveryPercent": 0.3,
    "isMetal": true,
    "metalType": "meteoricsteel"
  }
]
```
### mngeology: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/mngeology/patches/whetstone-additions.json`
```json
[
  {
    "op": "addeach",
    "path": "/variantgroups/0/states/-",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": [
      "komatiite",
      "monzonite",
      "pyroxenite",
      "serpentinite",
      "syenite"
    ]
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-komatiite",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/komatiite-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-pyroxenite",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/pyroxenite-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-serpentinite",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/serpentinite-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-monzonite",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/monzonite-inside"
      }
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType/*-syenite",
    "file": "toolsmith:itemtypes/whetstone.json",
    "value": {
      "stone": {
        "base": "game:block/stone/polishedrock/syenite-inside"
      }
    }
  }
]
```
`assets/toolsmith/compatibility/mngeology/recipes/knapping/whetstone.json`
```json
[
  {
    "ingredient": {
      "type": "item",
      "code": "game:stone-*",
      "name": "rock",
      "allowedVariants": [
        "komatiite",
        "monzonite",
        "pyroxenite",
        "serpentinite",
        "syenite"
      ]
    },
    "pattern": [
      [
        "__________",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "__________"
      ]
    ],
    "name": "Stone whetstone",
    "output": {
      "type": "item",
      "code": "toolsmith:whetstone-{rock}"
    }
  }
]
```
### tailorsdelight: exact installed ZIP mod ID present = True
`assets/toolsmith/compatibility/tailorsdelight/patches/fabricstrips-additions.json`
```json
[
  {
    "op": "addeach",
    "path": "/behaviorsByType/*-cloth/0/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/fabricstrips.json",
    "value": [
      "tailorsdelight:block/cloth/dark/darkblue",
      "tailorsdelight:block/cloth/dark/darkbrown",
      "tailorsdelight:block/cloth/dark/darkgreen",
      "tailorsdelight:block/cloth/dark/darkred"
    ]
  }
]
```
`assets/toolsmith/compatibility/tailorsdelight/recipes/grid/fabricstrips-tailorsdelight-cloth.json`
```json
[
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkblue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkblue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkbrown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkbrown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkgreen"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkgreen"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkred"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkred"
          }
        }
      }
    }
  }
]
```
`assets/toolsmith/compatibility/tailorsdelight/recipes/grid/fabricstrips-tailorsdelight-leather.json`
```json
[
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkblue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/blue",
            "material-overlay": "tailorsdelight:overlays/darkblue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkbrown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/plain",
            "material-overlay": "tailorsdelight:overlays/darkbrown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkgreen"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/green",
            "material-overlay": "tailorsdelight:overlays/darkgreen"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkred"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/red",
            "material-overlay": "tailorsdelight:overlays/darkred"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-nadiyan"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/plain",
            "material-overlay": "tailorsdelight:overlays/lighten2"
          }
        }
      }
    }
  }
]
```
### uraniumexpanded: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-axe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(ferrousuranium)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.4
      },
      "scale": 3.2
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.43
      },
      "scale": 3.55
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-hammer.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/simple"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-hoe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(ferrousuranium)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -105
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.33
      },
      "scale": 2.9
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -125
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.38
      },
      "scale": 3.76
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-knife.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-pickaxe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-prospectingpick.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/simple"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-saw.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/simple"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-scythe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/ferrousuranium/vanilla-shovel.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-ferrousuranium",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/@shovelhead-(ferrousuranium)",
    "value": {
      "translation": {
        "x": -0.2,
        "y": -0.4,
        "z": -0.15
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/tpHandTransformByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.4,
        "z": -0.1
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/@shovel-(ferrousuranium)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/attributes/groundStorageTransformByType/*",
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-axe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/axe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(uraniumsteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.4
      },
      "scale": 3.2
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -70,
        "y": 70,
        "z": -50
      },
      "origin": {
        "x": 1.07,
        "y": 0,
        "z": 0.43
      },
      "scale": 3.55
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-hammer.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/simple"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-hoe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/hoe/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/@*-(uraniumsteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -105
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.33
      },
      "scale": 2.9
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/guiTransformByType/*",
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/guiTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -60,
        "z": -125
      },
      "origin": {
        "x": 0.95,
        "y": 0,
        "z": 0.38
      },
      "scale": 3.76
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-knife.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/knife/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-pickaxe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/pickaxe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-prospectingpick.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/simple"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-saw.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/simple"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-scythe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/patches/partshapes/uraniumsteel/vanilla-shovel.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType/*-uraniumsteel",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/advanced"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/shapeByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/shapeByType/*",
    "value": {
      "base": "toolsmith:item/parts/shovel/heads/nonmetal"
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/@shovelhead-(uraniumsteel)",
    "value": {
      "translation": {
        "x": -0.2,
        "y": -0.4,
        "z": -0.15
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/tpHandTransformByType/*",
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/tpHandTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.4,
        "z": -0.1
      },
      "rotation": {
        "x": 90,
        "y": 20,
        "z": 0
      },
      "scale": 0.85
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/@shovel-(uraniumsteel)",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/attributes/groundStorageTransformByType/*",
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/groundStorageTransformByType/*",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.5,
        "z": -0.15
      },
      "rotation": {
        "x": 13,
        "y": 88,
        "z": -87
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/config/toolsmith/parts/bindings/bindings-ferrousuranium.json`
```json
[
  {
    "id": "metalnailsandstrips-ferrousuranium",
    "bindingStatTag": "ferrousuraniumnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/config/toolsmith/parts/bindings/bindings-uraniumsteel.json`
```json
[
  {
    "id": "metalnailsandstrips-uraniumsteel",
    "bindingStatTag": "uraniumsteelnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/config/toolsmith/stats/bindings/bindings-ferrousuranium.json`
```json
[
  {
    "id": "ferrousuraniumnails",
    "texturePath": "game:block/metal/plate/ferrousuranium",
    "langTag": "binding-nails-ferrousuranium",
    "baseHPfactor": 1.7,
    "selfHPBonus": 0.28,
    "handleHPBonus": 0.18,
    "recoveryPercent": 0.48,
    "isMetal": true,
    "metalType": "ferrousuranium"
  }
]
```
`assets/toolsmith/compatibility/uraniumexpanded/config/toolsmith/stats/bindings/bindings-uraniumsteel.json`
```json
[
  {
    "id": "uraniumsteelnails",
    "texturePath": "game:block/metal/plate/uraniumsteel",
    "langTag": "binding-nails-uraniumsteel",
    "baseHPfactor": 2.35,
    "selfHPBonus": 0.7,
    "handleHPBonus": 0.48,
    "recoveryPercent": 0.28,
    "isMetal": true,
    "metalType": "uraniumsteel"
  }
]
```
### wildcrafttree: exact installed ZIP mod ID present = False
`assets/toolsmith/compatibility/wildcrafttree/patches/add-woods-to-handles.json`
```json
[
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/handle.json",
    "value": [
      "game:block/wood/debarked/cedar",
      "game:block/wood/debarked/fir",
      "game:block/wood/debarked/spruce",
      "game:block/wood/debarked/douglasfir",
      "game:block/wood/debarked/tuja",
      "game:block/wood/debarked/redcedar",
      "game:block/wood/debarked/yew",
      "game:block/wood/debarked/kauri",
      "game:block/wood/debarked/ginkgo",
      "game:block/wood/debarked/sycamore",
      "game:block/wood/debarked/honeylocust",
      "game:block/wood/debarked/dalbergia",
      "game:block/wood/debarked/umnini",
      "game:block/wood/debarked/banyan",
      "game:block/wood/debarked/elm",
      "game:block/wood/debarked/beech",
      "game:block/wood/debarked/bearnut",
      "game:block/wood/debarked/willow",
      "game:block/wood/debarked/poplar",
      "game:block/wood/debarked/tamanu",
      "game:block/wood/debarked/spurgetree",
      "game:block/wood/debarked/azobe",
      "game:block/wood/debarked/guajacum",
      "game:block/wood/debarked/eucalyptus",
      "game:block/wood/debarked/ghostgum",
      "game:block/wood/debarked/ohia",
      "game:block/wood/debarked/satinash",
      "game:block/wood/debarked/leadwood",
      "game:block/wood/debarked/bluemahoe",
      "game:block/wood/debarked/linden",
      "game:block/wood/debarked/sal",
      "game:block/wood/debarked/horsechestnut",
      "game:block/wood/debarked/tigerwood",
      "game:block/wood/debarked/mahogany",
      "game:block/wood/debarked/sapele",
      "game:block/wood/debarked/saxaul",
      "game:block/wood/debarked/ash",
      "game:block/wood/debarked/catalpa",
      "game:block/wood/debarked/jacaranda",
      "game:block/wood/debarked/mangrove",
      "game:block/wood/debarked/empresstree",
      "game:block/wood/debarked/charred",
      "game:block/wood/debarked/chlorociboria",
      "game:block/wood/debarked/petrified"
    ]
  },
  {
    "op": "addeach",
    "path": "/behaviors/1/properties/textures/0/values/-",
    "file": "toolsmith:itemtypes/handles/carpentedhandle.json",
    "value": [
      "game:block/wood/debarked/cedar",
      "game:block/wood/debarked/fir",
      "game:block/wood/debarked/spruce",
      "game:block/wood/debarked/douglasfir",
      "game:block/wood/debarked/tuja",
      "game:block/wood/debarked/redcedar",
      "game:block/wood/debarked/yew",
      "game:block/wood/debarked/kauri",
      "game:block/wood/debarked/ginkgo",
      "game:block/wood/debarked/sycamore",
      "game:block/wood/debarked/honeylocust",
      "game:block/wood/debarked/dalbergia",
      "game:block/wood/debarked/umnini",
      "game:block/wood/debarked/banyan",
      "game:block/wood/debarked/elm",
      "game:block/wood/debarked/beech",
      "game:block/wood/debarked/bearnut",
      "game:block/wood/debarked/willow",
      "game:block/wood/debarked/poplar",
      "game:block/wood/debarked/tamanu",
      "game:block/wood/debarked/spurgetree",
      "game:block/wood/debarked/azobe",
      "game:block/wood/debarked/guajacum",
      "game:block/wood/debarked/eucalyptus",
      "game:block/wood/debarked/ghostgum",
      "game:block/wood/debarked/ohia",
      "game:block/wood/debarked/satinash",
      "game:block/wood/debarked/leadwood",
      "game:block/wood/debarked/bluemahoe",
      "game:block/wood/debarked/linden",
      "game:block/wood/debarked/sal",
      "game:block/wood/debarked/horsechestnut",
      "game:block/wood/debarked/tigerwood",
      "game:block/wood/debarked/mahogany",
      "game:block/wood/debarked/sapele",
      "game:block/wood/debarked/saxaul",
      "game:block/wood/debarked/ash",
      "game:block/wood/debarked/catalpa",
      "game:block/wood/debarked/jacaranda",
      "game:block/wood/debarked/mangrove",
      "game:block/wood/debarked/empresstree",
      "game:block/wood/debarked/charred",
      "game:block/wood/debarked/chlorociboria",
      "game:block/wood/debarked/petrified"
    ]
  }
]
```
`assets/toolsmith/compatibility/wildcrafttree/recipes/grid/tree-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "wildcrafttree:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/wildcrafttree/recipes/grid/tree-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "wildcrafttree:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/wildcrafttree/recipes/grid/trees-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
`assets/toolsmith/compatibility/wildcrafttree/recipes/grid/trees-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### wool: exact installed ZIP mod ID present = True
`assets/toolsmith/compatibility/wool/patches/fabricstrips-additions.json`
```json
[
  {
    "op": "addmerge",
    "path": "/variantgroups/0/states/-",
    "file": "toolsmith:itemtypes/fabricstrips.json",
    "value": "wool"
  },
  {
    "op": "addmerge",
    "path": "/behaviorsByType/*-wool",
    "file": "toolsmith:itemtypes/fabricstrips.json",
    "value": [
      {
        "name": "toolsmith:ModularPartRenderingFromAttributes",
        "properties": {
          "textures": [
            {
              "code": "material",
              "Default": "wool:block/cloth/wool/plain",
              "values": [
                "wool:block/cloth/wool/plain",
                "game:block/cloth/linen/mordant",
                "wool:block/cloth/wool/blue",
                "wool:block/cloth/wool/red",
                "wool:block/cloth/wool/yellow",
                "wool:block/cloth/wool/green",
                "wool:block/cloth/wool/purple",
                "wool:block/cloth/wool/pink",
                "wool:block/cloth/wool/orange",
                "wool:block/cloth/wool/brown",
                "wool:block/cloth/wool/gray",
                "wool:block/cloth/wool/black",
                "wool:block/cloth/wool/white",
                "wool:block/cloth/wool/darkblue",
                "wool:block/cloth/wool/darkbrown",
                "wool:block/cloth/wool/darkgreen",
                "wool:block/cloth/wool/darkred"
              ]
            }
          ],
          "skipCreativeInventoryAdditions": false,
          "creativeTabs": [
            "general",
            "items"
          ]
        }
      }
    ]
  }
]
```
`assets/toolsmith/compatibility/wool/recipes/grid/fabricstrips-wool-cloth.json`
```json
[
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-plain"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/plain"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-mordant"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/mordant"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-blue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/blue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-red"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/red"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-yellow"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/yellow"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-green"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/green"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-purple"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/purple"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-pink"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/pink"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-orange"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/orange"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-brown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/brown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-gray"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/gray"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-black"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/black"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-white"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/white"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkblue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkblue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkbrown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkbrown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkgreen"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkgreen"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkred"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkred"
          }
        }
      }
    }
  }
]
```
## Exact attributed native destinations

See `attribute_inventory.json` for every defined concrete code, full creative attribute combinations, exact recursively sorted TreeAttribute page strings, and valid representative links. Active wool and Tailor’s Delight native patches are applied before expansion.

{"definedCodes": 35, "handbookCodes": 19, "nativeHandbookStacks": 85, "generatedAttributeStacks": 72}

VtmlUtil itemstack accepts codes only, not JsonItemStack attributes. Use single-quoted VTML href with raw JSON double quotes; escape the outer language/config JSON with a JSON serializer. Do NOT HTML-encode JSON quotes as &quot;: VtmlParser does not decode entities in attribute values. Use representative.richtextLink as real navigation; unattributed itemstack icons are decorative and may navigate to a missing bare page.

Internal `toolsmith:tinkertoolparts` is defined but has no native creative/handbook page; use a custom explanation destination. Non-north block orientations navigate to the corresponding north creative page.

## Exact recipes (native and conditional)

### `assets/toolsmith/recipes/grid/carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "game:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/recipes/grid/crudehandle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "item",
        "code": "game:firewood"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:crudehandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/recipes/grid/fabricstrips-cloth.json`
```json
[
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-plain"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/plain"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-mordant"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/mordant"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-blue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/blue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-red"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/red"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-yellow"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/yellow"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-green"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/green"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-purple"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/purple"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-pink"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/pink"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-orange"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/orange"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-brown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/brown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-gray"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/gray"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-black"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/black"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "game:cloth-white"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/white"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "block",
        "code": "game:linen-diamond-down"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/plain"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "block",
        "code": "game:linen-normal-down"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/plain"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "block",
        "code": "game:linen-offset-down"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/plain"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "block",
        "code": "game:linen-square-down"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/plain"
          }
        }
      }
    }
  }
]
```
### `assets/toolsmith/recipes/grid/fabricstrips-leather.json`
```json
[
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-plain"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/plain"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-orange"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/orange"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-black"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/black"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-red"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/red"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-blue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/blue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-purple"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/purple"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-pink"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/pink"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-white"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/white"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-yellow"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/yellow"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-gray"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/gray"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "game:leather-normal-green"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/green"
          }
        }
      }
    }
  }
]
```
### `assets/toolsmith/recipes/grid/fabricstrips-rawhide.json`
```json
[
  {
    "ingredientPattern": "KR",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "R": {
        "type": "item",
        "code": "game:hide-raw-small"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-rawhide",
      "quantity": 2
    }
  },
  {
    "ingredientPattern": "KR",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "R": {
        "type": "item",
        "code": "game:hide-raw-medium"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-rawhide",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "KR",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "R": {
        "type": "item",
        "code": "game:hide-raw-large"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-rawhide",
      "quantity": 6
    }
  },
  {
    "ingredientPattern": "KR",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "R": {
        "type": "item",
        "code": "game:hide-raw-huge"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-rawhide",
      "quantity": 10
    }
  }
]
```
### `assets/toolsmith/recipes/grid/fabricstrips-sturdy.json`
```json
[
  {
    "ingredientPattern": "KS",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "game:leather-sturdy-plain"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-sturdy",
      "quantity": 2
    }
  }
]
```
### `assets/toolsmith/recipes/grid/grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/recipes/grid/handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "game:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/recipes/grid/recyclehandles.json`
```json
[
  {
    "ingredientPattern": "KH",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "H": {
        "type": "item",
        "code": "toolsmith:crudehandle"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "game:stick",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "KH",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "H": {
        "type": "item",
        "code": "toolsmith:handle"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "game:firewood",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "KH",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "H": {
        "type": "item",
        "code": "toolsmith:carpentedhandle"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "game:firewood",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/recipes/grid/sandpaper.json`
```json
[
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:woodbucket"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "game:glueportion-pitch-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:bowl-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "game:glueportion-pitch-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:jug-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "game:glueportion-pitch-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  }
]
```
### `assets/toolsmith/recipes/grid/workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/recipes/knapping/whetstone.json`
```json
[
  {
    "ingredient": {
      "type": "item",
      "code": "game:stone-*",
      "name": "rock",
      "allowedVariants": [
        "chert",
        "granite",
        "andesite",
        "basalt",
        "peridotite"
      ]
    },
    "pattern": [
      [
        "__________",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "__________"
      ]
    ],
    "name": "Stone whetstone",
    "output": {
      "type": "item",
      "code": "toolsmith:whetstone-{rock}"
    }
  }
]
```
### `assets/toolsmith/compatibility/ancienttools/recipes/grid/fabricstrips-atleather-cloth.json`
```json
[
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-acacia"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/acacia"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-aged"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/aged"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-baldcypress"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/baldcypress"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-birch"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/birch"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-cypress"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/cypress"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-ebony"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/ebony"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-kapok"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/kapok"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-larch"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/larch"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-maple"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/maple"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-pine"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/pine"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-purpleheart"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/purpleheart"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-redwood"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/redwood"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:leather-normal-walnut"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/leather/walnut"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "ancienttools:buckskin"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-atleather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "ancienttools:item/resource/buckskin"
          }
        }
      }
    }
  }
]
```
### `assets/toolsmith/compatibility/butchering/recipes/grid/sandpaper-from-glues.json`
```json
[
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:woodbucket"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-sinew-cold"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:bowl-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-sinew-cold"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:jug-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-sinew-cold"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:woodbucket"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-hide-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:bowl-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-hide-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  },
  {
    "ingredientPattern": "SG,P_",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:sand-*"
      },
      "G": {
        "type": "block",
        "code": "game:jug-*-fired"
      },
      "P": {
        "type": "item",
        "code": "game:paper-parchment"
      }
    },
    "attributes": {
      "liquidContainerProps": {
        "requiresContent": {
          "type": "item",
          "code": "butchering:glueportion-hide-hot"
        },
        "requiresLitres": 0.5
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:sandpaper",
      "quantity": 4
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonescaperegion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonescaperegion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaperegion/recipes/grid/cape-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaperegion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonescaribbeanregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonescaribbeanregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescaribbeanregion/recipes/grid/caribbean-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonescaribbeanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonescentralaustralianregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonescentralaustralianregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonescentralaustralianregion/recipes/grid/centralaustralian-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonescentralaustralianregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonesmediterraneanregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonesmediterraneanregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesmediterraneanregion/recipes/grid/mediterranean-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonesmediterraneanregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "floralzonesneozeylandicregion:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "floralzonesneozeylandicregion:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/floralzonesneozeylandicregion/recipes/grid/neozeylandicregion-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "floralzonesneozeylandicregion:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/geoaddons/recipes/grid/grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-diorite"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-diorite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-gabbro"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-gabbro-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-quartzite"
      },
      "P": {
        "type": "item",
        "code": "game:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "game:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-quartzite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/geoaddons/recipes/knapping/whetstone.json`
```json
[
  {
    "ingredient": {
      "type": "item",
      "code": "game:stone-*",
      "name": "rock",
      "allowedVariants": [
        "diorite",
        "gabbro",
        "quartzite",
        "jade",
        "jasper"
      ]
    },
    "pattern": [
      [
        "__________",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "__________"
      ]
    ],
    "name": "Stone whetstone",
    "output": {
      "type": "item",
      "code": "toolsmith:whetstone-{rock}"
    }
  }
]
```
### `assets/toolsmith/compatibility/mngeology/recipes/knapping/whetstone.json`
```json
[
  {
    "ingredient": {
      "type": "item",
      "code": "game:stone-*",
      "name": "rock",
      "allowedVariants": [
        "komatiite",
        "monzonite",
        "pyroxenite",
        "serpentinite",
        "syenite"
      ]
    },
    "pattern": [
      [
        "__________",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "___####___",
        "__________"
      ]
    ],
    "name": "Stone whetstone",
    "output": {
      "type": "item",
      "code": "toolsmith:whetstone-{rock}"
    }
  }
]
```
### `assets/toolsmith/compatibility/tailorsdelight/recipes/grid/fabricstrips-tailorsdelight-cloth.json`
```json
[
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkblue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkblue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkbrown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkbrown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkgreen"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkgreen"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "tailorsdelight:cloth-darkred"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-cloth",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "tailorsdelight:block/cloth/dark/darkred"
          }
        }
      }
    }
  }
]
```
### `assets/toolsmith/compatibility/tailorsdelight/recipes/grid/fabricstrips-tailorsdelight-leather.json`
```json
[
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkblue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/blue",
            "material-overlay": "tailorsdelight:overlays/darkblue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkbrown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/plain",
            "material-overlay": "tailorsdelight:overlays/darkbrown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkgreen"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/green",
            "material-overlay": "tailorsdelight:overlays/darkgreen"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-darkred"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/red",
            "material-overlay": "tailorsdelight:overlays/darkred"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KL",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "L": {
        "type": "item",
        "code": "tailorsdelight:leather-nadiyan"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-leather",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/leather/plain",
            "material-overlay": "tailorsdelight:overlays/lighten2"
          }
        }
      }
    }
  }
]
```
### `assets/toolsmith/compatibility/wildcrafttree/recipes/grid/tree-carpentedhandle.json`
```json
[
  {
    "ingredientPattern": "KS,W_",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "S": {
        "type": "item",
        "code": "toolsmith:sandpaper"
      },
      "W": {
        "type": "block",
        "code": "wildcrafttree:supportbeam-*"
      }
    },
    "width": 2,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:carpentedhandle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/wildcrafttree/recipes/grid/tree-handle.json`
```json
[
  {
    "ingredientPattern": "K,W",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "W": {
        "type": "block",
        "code": "wildcrafttree:supportbeam-*"
      }
    },
    "width": 1,
    "height": 2,
    "output": {
      "type": "item",
      "code": "toolsmith:handle",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/wildcrafttree/recipes/grid/trees-grindstone.json`
```json
[
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-granite"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-granite-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-sandstone"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-sandstone-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-shale"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-shale-north",
      "quantity": 1
    }
  },
  {
    "ingredientPattern": "PSP,QQQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "block",
        "code": "game:rock-andesite"
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*"
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:grindstone-andesite-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/wildcrafttree/recipes/grid/trees-workbench.json`
```json
[
  {
    "ingredientPattern": "PPP,QSQ,Q_Q",
    "ingredients": {
      "S": {
        "type": "item",
        "code": "game:saw-*",
        "isTool": true
      },
      "P": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 3
      },
      "Q": {
        "type": "item",
        "code": "wildcrafttree:plank-*",
        "quantity": 2
      }
    },
    "width": 3,
    "height": 3,
    "output": {
      "type": "block",
      "code": "toolsmith:workbench-north",
      "quantity": 1
    }
  }
]
```
### `assets/toolsmith/compatibility/wool/recipes/grid/fabricstrips-wool-cloth.json`
```json
[
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-plain"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/plain"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-mordant"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "game:block/cloth/linen/mordant"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-blue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/blue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-red"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/red"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-yellow"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/yellow"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-green"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/green"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-purple"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/purple"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-pink"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/pink"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-orange"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/orange"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-brown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/brown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-gray"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/gray"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-black"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/black"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-white"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/white"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkblue"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkblue"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkbrown"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkbrown"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkgreen"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkgreen"
          }
        }
      }
    }
  },
  {
    "ingredientPattern": "KC",
    "ingredients": {
      "K": {
        "type": "item",
        "code": "game:knife-*",
        "isTool": true
      },
      "C": {
        "type": "item",
        "code": "wool:cloth-wool-darkred"
      }
    },
    "width": 2,
    "height": 1,
    "output": {
      "type": "item",
      "code": "toolsmith:fabricstrips-wool",
      "quantity": 2,
      "attributes": {
        "modularPartRenderData": {
          "partTextures": {
            "material": "wool:block/cloth/wool/darkred"
          }
        }
      }
    }
  }
]
```
## Part definitions, stat tables, classification, patches

### `assets/toolsmith/patches/coldforgemetals.json`
```json
[
  {
    "op": "addmerge",
    "path": "/attributes/workableTemperatureByType/ingot-copper",
    "value": 0,
    "file": "game:itemtypes/resource/ingot.json"
  },
  {
    "op": "addmerge",
    "path": "/attributes/workableTemperatureByType",
    "value": {},
    "file": "game:itemtypes/resource/metalplate.json"
  },
  {
    "op": "addmerge",
    "path": "/attributes/workableTemperatureByType/metalplate-copper",
    "value": 0,
    "file": "game:itemtypes/resource/metalplate.json"
  },
  {
    "op": "addmerge",
    "path": "/attributes/workableTemperatureByType/metalplate-lead",
    "value": 0,
    "file": "game:itemtypes/resource/metalplate.json"
  },
  {
    "op": "addmerge",
    "path": "/attributes/workableTemperatureByType/metalplate-tin",
    "value": 0,
    "file": "game:itemtypes/resource/metalplate.json"
  },
  {
    "op": "addmerge",
    "path": "/attributesByType/workitem-copper",
    "value": {
      "workableTemperature": 0
    },
    "file": "game:itemtypes/resource/workitem.json"
  }
]
```
### `assets/toolsmith/patches/supportbeam-output-to-4.json`
```json
[
  {
    "file": "game:recipes/grid/supportbeams.json",
    "op": "replace",
    "path": "/0/output/quantity",
    "value": 4,
    "enabled": true,
    "side": "server"
  }
]
```
### `assets/toolsmith/patches/workablebits.json`
```json
[
  {
    "file": "game:itemtypes/resource/nugget.json",
    "op": "addmerge",
    "path": "/classByType",
    "value": {
      "@*-(limonite|hematite|magnetite)": "ItemNugget"
    },
    "side": "Server",
    "condition": {
      "when": "Toolsmith_SmithWithBits",
      "isValue": "true"
    }
  },
  {
    "file": "game:itemtypes/resource/nugget.json",
    "op": "addmerge",
    "path": "/classByType",
    "value": {
      "*": "toolsmith:WorkableBits"
    },
    "side": "Server",
    "condition": {
      "when": "Toolsmith_SmithWithBits",
      "isValue": "true"
    }
  },
  {
    "file": "game:itemtypes/resource/metalbit.json",
    "op": "replace",
    "path": "/class",
    "value": "toolsmith:WorkableBits",
    "side": "Server",
    "condition": {
      "when": "Toolsmith_SmithWithBits",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/animations/crafting-animations-player-entity.json`
```json
[
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "crafting",
      "animation": "crafting",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "Head": 20,
        "UpperTorso": 20,
        "LowerTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20,
        "UpperFootL": 20,
        "LowerFootL": 20,
        "UpperFootR": 20,
        "LowerFootR": 20
      },
      "elementBlendMode": {
        "Head": "AddAverage",
        "UpperTorso": "AddAverage",
        "LowerTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage",
        "UpperFootL": "AddAverage",
        "LowerFootL": "AddAverage",
        "UpperFootR": "AddAverage",
        "LowerFootR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  },
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "crafting-fp",
      "animation": "crafting-fp",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "Head": 20,
        "UpperTorso": 20,
        "LowerTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20,
        "UpperFootL": 20,
        "LowerFootL": 20,
        "UpperFootR": 20,
        "LowerFootR": 20
      },
      "elementBlendMode": {
        "Head": "AddAverage",
        "UpperTorso": "AddAverage",
        "LowerTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage",
        "UpperFootL": "AddAverage",
        "LowerFootL": "AddAverage",
        "UpperFootR": "AddAverage",
        "LowerFootR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  },
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "craftingwinding",
      "animation": "craftingwinding",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "Head": 20,
        "UpperTorso": 20,
        "LowerTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20,
        "UpperFootL": 20,
        "LowerFootL": 20,
        "UpperFootR": 20,
        "LowerFootR": 20
      },
      "elementBlendMode": {
        "Head": "AddAverage",
        "UpperTorso": "AddAverage",
        "LowerTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage",
        "UpperFootL": "AddAverage",
        "LowerFootL": "AddAverage",
        "UpperFootR": "AddAverage",
        "LowerFootR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  },
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "craftingwinding-fp",
      "animation": "craftingwinding-fp",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "UpperTorso": 20,
        "LowerTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20,
        "UpperFootL": 20,
        "LowerFootL": 20,
        "UpperFootR": 20,
        "LowerFootR": 20
      },
      "elementBlendMode": {
        "UpperTorso": "AddAverage",
        "LowerTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage",
        "UpperFootL": "AddAverage",
        "LowerFootL": "AddAverage",
        "UpperFootR": "AddAverage",
        "LowerFootR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  },
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "sharpeningstone",
      "animation": "sharpeningstone",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "Head": 20,
        "UpperTorso": 20,
        "LowerTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20,
        "UpperFootL": 20,
        "LowerFootL": 20,
        "UpperFootR": 20,
        "LowerFootR": 20
      },
      "elementBlendMode": {
        "Head": "AddAverage",
        "UpperTorso": "AddAverage",
        "LowerTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage",
        "UpperFootL": "AddAverage",
        "LowerFootL": "AddAverage",
        "UpperFootR": "AddAverage",
        "LowerFootR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  },
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "sharpeningstone-fp",
      "animation": "sharpeningstone-fp",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "Head": 20,
        "UpperTorso": 20,
        "LowerTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20,
        "UpperFootL": 20,
        "LowerFootL": 20,
        "UpperFootR": 20,
        "LowerFootR": 20
      },
      "elementBlendMode": {
        "Head": "AddAverage",
        "UpperTorso": "AddAverage",
        "LowerTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage",
        "UpperFootL": "AddAverage",
        "LowerFootL": "AddAverage",
        "UpperFootR": "AddAverage",
        "LowerFootR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  },
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "grindingwheel",
      "animation": "grindingwheel",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "ItemAnchor": 80,
        "UpperTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20
      },
      "elementBlendMode": {
        "ItemAnchor": "AddAverage",
        "UpperTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  },
  {
    "op": "addmerge",
    "path": "/client/animations/-",
    "value": {
      "code": "grindingwheel-fp",
      "animation": "grindingwheel-fp",
      "easeInSpeed": 1,
      "blendMode": "Average",
      "elementWeight": {
        "ItemAnchor": 20,
        "UpperTorso": 20,
        "UpperArmL": 20,
        "LowerArmL": 20,
        "UpperArmR": 20,
        "LowerArmR": 20
      },
      "elementBlendMode": {
        "ItemAnchor": "AddAverage",
        "UpperTorso": "AddAverage",
        "UpperArmL": "AddAverage",
        "LowerArmL": "AddAverage",
        "UpperArmR": "AddAverage",
        "LowerArmR": "AddAverage"
      }
    },
    "file": "game:entities/humanoid/player.json"
  }
]
```
### `assets/toolsmith/patches/animations/crafting-animations-player-shape.json`
```json
[
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "crafting",
      "code": "crafting",
      "quantityframes": 26,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0292,
              "offsetY": -0.133,
              "offsetZ": -0.1728,
              "rotationX": -74.2433,
              "rotationY": -6.9001,
              "rotationZ": -100.1423
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 34.8833,
              "rotationY": -12.3861,
              "rotationZ": -37.8014
            },
            "LowerArmR": {
              "offsetX": 0.0372,
              "offsetY": 0.0638,
              "offsetZ": 0.2072,
              "rotationX": -123.9459,
              "rotationY": -4.3279,
              "rotationZ": -54.0964
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -11.466,
              "rotationY": 21.8941,
              "rotationZ": -19.9504
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.1701,
              "rotationY": -3.243,
              "rotationZ": 24.1903
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.2963,
              "rotationY": 3.5038,
              "rotationZ": -0.0703
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            }
          }
        },
        {
          "frame": 5,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.06,
              "offsetY": -0.1705,
              "offsetZ": -0.3209,
              "rotationX": -73.6622,
              "rotationY": -6.0173,
              "rotationZ": -101.2458
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 27.9126,
              "rotationY": -17.91,
              "rotationZ": -50.6735
            },
            "LowerArmR": {
              "offsetX": -0.0373,
              "offsetY": -0.1161,
              "offsetZ": 0.3475,
              "rotationX": -121.2186,
              "rotationY": -34.5433,
              "rotationZ": -33.9321
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -5.3997,
              "rotationY": 19.763,
              "rotationZ": -20.7102
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.1951
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0206,
              "offsetY": -0.1513,
              "offsetZ": -0.3952,
              "rotationX": -74.2433,
              "rotationY": -6.9001,
              "rotationZ": -100.1423
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 46.5168,
              "rotationY": 7.9478,
              "rotationZ": -62.1594
            },
            "LowerArmR": {
              "offsetX": -0.1159,
              "offsetY": 0.0506,
              "offsetZ": 0.4043,
              "rotationX": -96.0204,
              "rotationY": -3.8443,
              "rotationZ": -51.3773
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -11.466,
              "rotationY": 21.8941,
              "rotationZ": -19.9504
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.1701,
              "rotationY": -3.243,
              "rotationZ": 24.1903
            }
          }
        },
        {
          "frame": 15,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.072,
              "offsetY": -0.1793,
              "offsetZ": -0.4923,
              "rotationX": -73.6622,
              "rotationY": -6.0173,
              "rotationZ": -101.2458
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 36.982,
              "rotationY": 3.7868,
              "rotationZ": -70.3549
            },
            "LowerArmR": {
              "offsetX": 0.2801,
              "offsetY": -0.3221,
              "offsetZ": 0.3123,
              "rotationX": -142.0693,
              "rotationY": -40.7516,
              "rotationZ": -58.2988
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -5.3997,
              "rotationY": 19.763,
              "rotationZ": -20.7102
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.1951
            }
          }
        },
        {
          "frame": 20,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0269,
              "offsetY": -0.144,
              "offsetZ": -0.2613,
              "rotationX": -74.2433,
              "rotationY": -6.9001,
              "rotationZ": -100.1423
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 27.9126,
              "rotationY": -17.91,
              "rotationZ": -50.6735
            },
            "LowerArmR": {
              "offsetX": -0.0334,
              "offsetY": -0.0552,
              "offsetZ": 0.2925,
              "rotationX": -119.9661,
              "rotationY": -22.3355,
              "rotationZ": -36.9776
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -11.466,
              "rotationY": 21.8941,
              "rotationZ": -19.9504
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.1701,
              "rotationY": -3.243,
              "rotationZ": 24.1903
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.2963,
              "rotationY": 3.5038,
              "rotationZ": -0.0703
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            }
          }
        },
        {
          "frame": 25,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0292,
              "offsetY": -0.133,
              "offsetZ": -0.1728,
              "rotationX": -74.2433,
              "rotationY": -6.9001,
              "rotationZ": -100.1423
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 34.8833,
              "rotationY": -12.3861,
              "rotationZ": -37.8014
            },
            "LowerArmR": {
              "offsetX": 0.0372,
              "offsetY": 0.0638,
              "offsetZ": 0.2072,
              "rotationX": -123.9459,
              "rotationY": -4.3279,
              "rotationZ": -54.0964
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  },
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "crafting-fp",
      "code": "crafting-fp",
      "quantityframes": 21,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -76.4035,
              "rotationY": -16.6711,
              "rotationZ": -100.8496
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -113.3072,
              "rotationY": -29.0228,
              "rotationZ": -77.9327
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.1701,
              "rotationY": -3.243,
              "rotationZ": 24.1903
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.2963,
              "rotationY": 3.5038,
              "rotationZ": -0.0703
            },
            "UpperArmR": {
              "offsetX": -0.592,
              "offsetY": -1.2714,
              "offsetZ": -2.2757,
              "rotationX": 29.3792,
              "rotationY": -23.5786,
              "rotationZ": -100.2135
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.5441,
              "rotationY": -0.5979,
              "rotationZ": 108.1268
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperArmL": {
              "offsetX": -1.823,
              "offsetY": 1.1456,
              "offsetZ": 0.2276,
              "rotationX": 12.594,
              "rotationY": 31.9677,
              "rotationZ": -75.4055
            }
          }
        },
        {
          "frame": 5,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -76.1141,
              "rotationY": -15.6774,
              "rotationZ": -100.7597
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -71.5895,
              "rotationY": -15.315,
              "rotationZ": -64.4589
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.1951
            },
            "UpperArmR": {
              "offsetX": -0.4078,
              "offsetY": -1.1977,
              "offsetZ": -2.3548,
              "rotationX": 42.8694,
              "rotationY": -25.1534,
              "rotationZ": -85.2242
            },
            "UpperArmL": {
              "rotationX": 16.1887,
              "rotationY": 24.1288,
              "rotationZ": -74.7325
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -76.4035,
              "rotationY": -16.6711,
              "rotationZ": -100.8496
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -113.9393,
              "rotationY": -28.283,
              "rotationZ": -76.9414
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 20.1951
            },
            "UpperArmR": {
              "offsetX": -0.8759,
              "offsetY": -1.369,
              "offsetZ": -2.1987,
              "rotationX": 25.1217,
              "rotationY": -23.418,
              "rotationZ": -97.2667
            },
            "UpperArmL": {
              "rotationX": 14.4625,
              "rotationY": 29.9871,
              "rotationZ": -75.3454
            }
          }
        },
        {
          "frame": 15,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -76.1141,
              "rotationY": -15.6774,
              "rotationZ": -100.7597
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -71.5895,
              "rotationY": -15.315,
              "rotationZ": -64.4589
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.1951
            },
            "UpperArmR": {
              "offsetX": -0.4078,
              "offsetY": -1.1977,
              "offsetZ": -2.3548,
              "rotationX": 42.8694,
              "rotationY": -25.1534,
              "rotationZ": -85.2242
            },
            "UpperArmL": {
              "rotationX": 16.1887,
              "rotationY": 24.1288,
              "rotationZ": -74.7325
            }
          }
        },
        {
          "frame": 20,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -76.4035,
              "rotationY": -16.6711,
              "rotationZ": -100.8496
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -113.3072,
              "rotationY": -29.0228,
              "rotationZ": -77.9327
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.1701,
              "rotationY": -3.243,
              "rotationZ": 24.1903
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.2963,
              "rotationY": 3.5038,
              "rotationZ": -0.0703
            },
            "UpperArmR": {
              "offsetX": -0.592,
              "offsetY": -1.2714,
              "offsetZ": -2.2757,
              "rotationX": 29.3792,
              "rotationY": -23.5786,
              "rotationZ": -100.2135
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.5441,
              "rotationY": -0.5979,
              "rotationZ": 108.1268
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperArmL": {
              "rotationX": 12.594,
              "rotationY": 31.9677,
              "rotationZ": -75.4055
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  },
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "craftingwinding",
      "code": "craftingwinding",
      "quantityframes": 16,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 113.2638,
              "rotationY": 48.5849,
              "rotationZ": -58.9818
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -34.9892,
              "rotationY": 15.2486,
              "rotationZ": -62.7759
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.4909
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 76.4034,
              "rotationY": 16.671,
              "rotationZ": -100.8496
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 3.793,
              "rotationY": 9.5754,
              "rotationZ": 11.761
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 11.466,
              "rotationY": -21.8941,
              "rotationZ": -19.9504
            }
          }
        },
        {
          "frame": 5,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 107.7689,
              "rotationY": 30.9495,
              "rotationZ": -43.6123
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -28.4211,
              "rotationY": 6.1576,
              "rotationZ": -23.2863
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 78.0858,
              "rotationY": 16.0396,
              "rotationZ": -108.1698
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 10.3123,
              "rotationY": -29.3648,
              "rotationZ": -16.7911
            }
          }
        },
        {
          "frame": 8,
          "elements": {
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.1047,
              "rotationY": -1.9973,
              "rotationZ": 18.489
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 114.2833,
              "rotationY": 28.1025,
              "rotationZ": -6.546
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -34.2547,
              "rotationY": 26.8157,
              "rotationZ": -50.6123
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 76.4034,
              "rotationY": 16.671,
              "rotationZ": -100.8496
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 11.466,
              "rotationY": -21.8941,
              "rotationZ": -19.9504
            }
          }
        },
        {
          "frame": 15,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 113.2638,
              "rotationY": 48.5849,
              "rotationZ": -58.9818
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -34.9892,
              "rotationY": 15.2486,
              "rotationZ": -62.7759
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.4909
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 76.4034,
              "rotationY": 16.671,
              "rotationZ": -100.8496
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 3.793,
              "rotationY": 9.5754,
              "rotationZ": 11.761
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 11.466,
              "rotationY": -21.8941,
              "rotationZ": -19.9504
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  },
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "craftingwinding-fp",
      "code": "craftingwinding-fp",
      "quantityframes": 16,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 20.4909
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 76.4034,
              "rotationY": 16.671,
              "rotationZ": -100.8496
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperArmL": {
              "offsetX": 1.8695,
              "offsetY": 0.2608,
              "offsetZ": 0.89,
              "rotationX": 11.1637,
              "rotationY": 46.9621,
              "rotationZ": -139.627
            },
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 113.2638,
              "rotationY": 48.5849,
              "rotationZ": -58.9818
            },
            "UpperArmR": {
              "offsetX": 1.0916,
              "offsetY": -0.9328,
              "offsetZ": -1.5145,
              "rotationX": -25.1789,
              "rotationY": -26.5266,
              "rotationZ": -97.7475
            }
          }
        },
        {
          "frame": 5,
          "elements": {
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.1048,
              "rotationY": -2.9982,
              "rotationZ": 25.4936
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 78.0858,
              "rotationY": 16.0396,
              "rotationZ": -108.1698
            },
            "UpperArmL": {
              "offsetX": 1.4569,
              "offsetY": -1.0448,
              "offsetZ": 0.8015,
              "rotationX": -0.5953,
              "rotationY": 40.7984,
              "rotationZ": -91.972
            },
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 107.7689,
              "rotationY": 30.9495,
              "rotationZ": -43.6123
            },
            "UpperArmR": {
              "offsetX": 0.9217,
              "offsetY": -1.1032,
              "offsetZ": -1.6871,
              "rotationX": -35.1366,
              "rotationY": -24.4171,
              "rotationZ": -96.56
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.1572,
              "rotationY": -2.9959,
              "rotationZ": 20.4868
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 76.4034,
              "rotationY": 16.671,
              "rotationZ": -100.8496
            },
            "UpperArmL": {
              "offsetX": 1.5515,
              "offsetY": 0.0528,
              "offsetZ": 1.3469,
              "rotationX": 27.471,
              "rotationY": 48.1578,
              "rotationZ": -139.9932
            },
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 114.2833,
              "rotationY": 28.1025,
              "rotationZ": -6.546
            },
            "UpperArmR": {
              "offsetX": 1.0957,
              "offsetY": -1.0941,
              "offsetZ": -1.4589,
              "rotationX": -26.0261,
              "rotationY": -26.0708,
              "rotationZ": -97.5744
            }
          }
        },
        {
          "frame": 15,
          "elements": {
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 20.4909
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 76.4034,
              "rotationY": 16.671,
              "rotationZ": -100.8496
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperArmL": {
              "offsetX": 1.8695,
              "offsetY": 0.2608,
              "offsetZ": 0.89,
              "rotationX": 11.1637,
              "rotationY": 46.9621,
              "rotationZ": -139.627
            },
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 113.2638,
              "rotationY": 48.5849,
              "rotationZ": -58.9818
            },
            "UpperArmR": {
              "offsetX": 1.0916,
              "offsetY": -0.9328,
              "offsetZ": -1.5145,
              "rotationX": -25.1789,
              "rotationY": -26.5266,
              "rotationZ": -97.7475
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  },
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "sharpeningstone",
      "code": "sharpeningstone",
      "quantityframes": 21,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -74.8107,
              "rotationY": -30.4798,
              "rotationZ": -69.876
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 31.464,
              "rotationY": 19.6994,
              "rotationZ": -58.776
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -111.0657,
              "rotationY": -6.7831,
              "rotationZ": -43.4236
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -3.3674,
              "rotationY": 32.3225,
              "rotationZ": -60.6506
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.2623,
              "rotationY": -4.9931,
              "rotationZ": 24.2076
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 12.5402,
              "rotationY": 18.3458,
              "rotationZ": -2.0328
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -84.0823,
              "rotationY": 2.4215,
              "rotationZ": -73.1443
            }
          }
        },
        {
          "frame": 15,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -90.555,
              "rotationY": 9.6055,
              "rotationZ": -63.6058
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 27.7479,
              "rotationY": 14.815,
              "rotationZ": -54.5471
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -5.7162,
              "rotationY": 1.0736,
              "rotationZ": -32.1187
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.1951
            }
          }
        },
        {
          "frame": 17,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -83.1445,
              "rotationY": 0.01,
              "rotationZ": -73.0155
            }
          }
        },
        {
          "frame": 20,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -74.8107,
              "rotationY": -30.4798,
              "rotationZ": -69.876
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 31.464,
              "rotationY": 19.6994,
              "rotationZ": -58.776
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -3.3674,
              "rotationY": 32.3225,
              "rotationZ": -60.6506
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.2623,
              "rotationY": -4.9931,
              "rotationZ": 24.2076
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 12.5402,
              "rotationY": 18.3458,
              "rotationZ": -2.0328
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  },
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "sharpeningstone-fp",
      "code": "sharpeningstone-fp",
      "quantityframes": 21,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -74.8107,
              "rotationY": -30.4798,
              "rotationZ": -69.876
            },
            "UpperArmR": {
              "offsetX": 1.2405,
              "offsetY": -0.6662,
              "offsetZ": -1.4263,
              "rotationX": 31.783,
              "rotationY": -6.454,
              "rotationZ": -89.3461
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -111.0657,
              "rotationY": -6.7831,
              "rotationZ": -43.4236
            },
            "UpperArmL": {
              "offsetX": -0.0031,
              "offsetY": 0.0152,
              "offsetZ": 2.0042,
              "rotationX": 8.9279,
              "rotationY": 6.7612,
              "rotationZ": -106.271
            },
            "UpperFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 2.7347,
              "rotationY": 2.9919,
              "rotationZ": -84.8547
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.2623,
              "rotationY": -4.9931,
              "rotationZ": 24.2076
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 12.5402,
              "rotationY": 18.3458,
              "rotationZ": -2.0328
            },
            "LowerFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.9851,
              "rotationY": 0.7613,
              "rotationZ": 74.9844
            },
            "UpperFootR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "LowerFootL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 1.335,
              "rotationY": -0.7009,
              "rotationZ": 94.982
            },
            "LowerTorso": {
              "offsetX": -0.2646,
              "offsetY": -5.0481,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -84.0823,
              "rotationY": 2.4215,
              "rotationZ": -73.1443
            }
          }
        },
        {
          "frame": 15,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -90.555,
              "rotationY": 9.6055,
              "rotationZ": -63.6058
            },
            "UpperArmR": {
              "offsetX": 1.1367,
              "offsetY": -0.6175,
              "offsetZ": -1.5253,
              "rotationX": 28.9417,
              "rotationY": -10.6348,
              "rotationZ": -85.6879
            },
            "UpperArmL": {
              "offsetX": 0.8891,
              "offsetY": 0.283,
              "offsetZ": 1.769,
              "rotationX": -12.9123,
              "rotationY": -15.864,
              "rotationZ": -79.3615
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 24.1951
            }
          }
        },
        {
          "frame": 17,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -83.1445,
              "rotationY": 0.01,
              "rotationZ": -73.0155
            }
          }
        },
        {
          "frame": 20,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -74.8107,
              "rotationY": -30.4798,
              "rotationZ": -69.876
            },
            "UpperArmR": {
              "offsetX": 1.2405,
              "offsetY": -0.6662,
              "offsetZ": -1.4263,
              "rotationX": 31.783,
              "rotationY": -6.454,
              "rotationZ": -89.3461
            },
            "UpperArmL": {
              "offsetX": -0.0031,
              "offsetY": 0.0152,
              "offsetZ": 2.0042,
              "rotationX": 8.9279,
              "rotationY": 6.7612,
              "rotationZ": -106.271
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -0.2623,
              "rotationY": -4.9931,
              "rotationZ": 24.2076
            },
            "Head": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 12.5402,
              "rotationY": 18.3458,
              "rotationZ": -2.0328
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  },
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "grindingwheel",
      "code": "grindingwheel",
      "quantityframes": 21,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -11.1947,
              "rotationY": -15.121,
              "rotationZ": -54.8547
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 72.3842,
              "rotationY": 59.6505,
              "rotationZ": -82.5901
            },
            "UpperArmR": {
              "offsetX": 0.1953,
              "offsetY": 0.1774,
              "offsetZ": -0.0505,
              "rotationX": -6.3073,
              "rotationY": -15.4762,
              "rotationZ": -59.0861
            },
            "UpperArmL": {
              "offsetX": -0.2318,
              "offsetY": -0.1346,
              "offsetZ": 0.0182,
              "rotationX": 1.613,
              "rotationY": -5.741,
              "rotationZ": -49.6345
            },
            "ItemAnchor": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 15.0
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -12.2229,
              "rotationY": -14.601,
              "rotationZ": -60.3211
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 73.7705,
              "rotationY": 56.2353,
              "rotationZ": -84.3134
            },
            "UpperArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -9.1042,
              "rotationY": -3.0388,
              "rotationZ": -58.0506
            },
            "UpperArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -2.3248,
              "rotationY": 5.2694,
              "rotationZ": -46.4518
            },
            "ItemAnchor": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -3.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 17.0
            }
          }
        },
        {
          "frame": 20,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -11.1947,
              "rotationY": -15.121,
              "rotationZ": -54.8547
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 72.3842,
              "rotationY": 59.6505,
              "rotationZ": -82.5901
            },
            "UpperArmR": {
              "offsetX": 0.1953,
              "offsetY": 0.1774,
              "offsetZ": -0.0505,
              "rotationX": -6.3073,
              "rotationY": -15.4762,
              "rotationZ": -59.0861
            },
            "UpperArmL": {
              "offsetX": -0.2318,
              "offsetY": -0.1346,
              "offsetZ": 0.0182,
              "rotationX": 1.613,
              "rotationY": -5.741,
              "rotationZ": -49.6345
            },
            "ItemAnchor": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 15.0
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  },
  {
    "op": "add",
    "path": "/animations/-",
    "value": {
      "name": "grindingwheel-fp",
      "code": "grindingwheel-fp",
      "quantityframes": 21,
      "onActivityStopped": "Rewind",
      "onAnimationEnd": "Repeat",
      "keyframes": [
        {
          "frame": 0,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -11.1947,
              "rotationY": -15.121,
              "rotationZ": -54.8547
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 72.3842,
              "rotationY": 59.6505,
              "rotationZ": -82.5901
            },
            "UpperArmR": {
              "offsetX": -0.0201,
              "offsetY": -0.3115,
              "offsetZ": -1.9771,
              "rotationX": -17.3451,
              "rotationY": -14.1409,
              "rotationZ": -100.5003
            },
            "UpperArmL": {
              "offsetX": 0.1117,
              "offsetY": -0.2155,
              "offsetZ": 1.9869,
              "rotationX": -0.1076,
              "rotationY": 0.9544,
              "rotationZ": -89.1288
            },
            "ItemAnchor": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 15.0
            }
          }
        },
        {
          "frame": 10,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -12.2229,
              "rotationY": -14.601,
              "rotationZ": -60.3211
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 73.7705,
              "rotationY": 56.2353,
              "rotationZ": -84.3134
            },
            "UpperArmR": {
              "offsetX": -0.0421,
              "offsetY": -0.1013,
              "offsetZ": -1.997,
              "rotationX": -11.2689,
              "rotationY": -2.903,
              "rotationZ": -98.1065
            },
            "UpperArmL": {
              "offsetX": 0.1374,
              "offsetY": -0.4393,
              "offsetZ": 1.9463,
              "rotationX": 3.9102,
              "rotationY": 11.9453,
              "rotationZ": -86.3834
            },
            "ItemAnchor": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -3.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 17.0
            }
          }
        },
        {
          "frame": 20,
          "elements": {
            "LowerArmL": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": -11.1947,
              "rotationY": -15.121,
              "rotationZ": -54.8547
            },
            "LowerArmR": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 72.3842,
              "rotationY": 59.6505,
              "rotationZ": -82.5901
            },
            "UpperArmR": {
              "offsetX": -0.0201,
              "offsetY": -0.3115,
              "offsetZ": -1.9771,
              "rotationX": -17.3451,
              "rotationY": -14.1409,
              "rotationZ": -100.5003
            },
            "UpperArmL": {
              "offsetX": 0.1117,
              "offsetY": -0.2155,
              "offsetZ": 1.9869,
              "rotationX": -0.1076,
              "rotationY": 0.9544,
              "rotationZ": -89.1288
            },
            "ItemAnchor": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 0.0
            },
            "UpperTorso": {
              "offsetX": 0.0,
              "offsetY": 0.0,
              "offsetZ": 0.0,
              "rotationX": 0.0,
              "rotationY": 0.0,
              "rotationZ": 15.0
            }
          }
        }
      ]
    },
    "file": "game:shapes/entity/humanoid/seraph-faceless.json"
  }
]
```
### `assets/toolsmith/patches/compatability/in-dappled-groves-disable-supportbeam-patch.json`
```json
[
  {
    "file": "toolsmith:patches/supportbeam-output-to-4.json",
    "op": "replace",
    "path": "/0/enabled",
    "value": "false",
    "side": "server",
    "dependsOn": [
      {
        "modid": "indappledgroves"
      }
    ]
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-axe.json`
```json
[
  {
    "op": "replace",
    "path": "/shapeByType",
    "value": {
      "*-copper": {
        "base": "toolsmith:item/parts/axe/heads/simple"
      },
      "*-tinbronze": {
        "base": "toolsmith:item/parts/axe/heads/normal"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/axe/heads/normal"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/axe/heads/normal"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/axe/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/axe/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/axe/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/axe/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/axe/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/axe/heads/nonmetal"
      }
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransformByType",
    "value": {
      "@*-(gold|silver|iron|meteoriciron|steel)": {
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -70,
          "y": 70,
          "z": -50
        },
        "origin": {
          "x": 1.07,
          "y": 0,
          "z": 0.4
        },
        "scale": 3.2
      },
      "@*-(copper|tinbronze|bismuthbronze|blackbronze)": {
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -70,
          "y": 70,
          "z": -50
        },
        "origin": {
          "x": 1.07,
          "y": 0,
          "z": 0.43
        },
        "scale": 3.2
      },
      "*": {
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -70,
          "y": 70,
          "z": -50
        },
        "origin": {
          "x": 1.07,
          "y": 0,
          "z": 0.43
        },
        "scale": 3.55
      }
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": -0.35,
        "y": -0.25,
        "z": -0.6
      },
      "rotation": {
        "x": 0,
        "y": -90,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0.5,
        "z": 0.9
      },
      "scale": 0.8
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transformByType",
    "value": {
      "@*-(copper|tinbronze|bismuthbronze|blackbronze)": {
        "translation": {
          "x": -0.44,
          "y": 0,
          "z": 0.27
        },
        "rotation": {
          "x": 0,
          "y": 25,
          "z": 0
        }
      },
      "@*-(gold|silver|iron|meteoriciron|steel)": {
        "translation": {
          "x": -0.32,
          "y": 0,
          "z": 0.44
        },
        "rotation": {
          "x": 0,
          "y": 45,
          "z": 0
        }
      },
      "*": {
        "translation": {
          "x": 0.53,
          "y": -0.1,
          "z": 0.05
        }
      }
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes/onshelfTransform",
    "value": {
      "translation": {
        "x": 0.4,
        "y": -0.0,
        "z": 0.3
      },
      "rotation": {
        "x": 0,
        "y": 135,
        "z": 0
      }
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onTongTransformByType",
    "value": {
      "@axehead-(copper|bismuthbronze|tinbronze|blackbronze)": {
        "translation": {
          "x": -0.75,
          "y": 0,
          "z": -0.375
        },
        "rotation": {
          "x": 0,
          "y": -155,
          "z": -55
        }
      },
      "*": {
        "translation": {
          "x": -0.75,
          "y": 0,
          "z": -0.375
        },
        "rotation": {
          "x": 0,
          "y": -155,
          "z": -55
        }
      }
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onMetalTongTransformByType",
    "value": {
      "@axehead-(copper|bismuthbronze|tinbronze|blackbronze)": {
        "translation": {
          "x": -1.6,
          "y": -0.22,
          "z": -0.55
        },
        "rotation": {
          "x": 0,
          "y": -175,
          "z": 5
        }
      },
      "*": {
        "translation": {
          "x": -1.6,
          "y": -0.22,
          "z": -0.55
        },
        "rotation": {
          "x": 0,
          "y": -175,
          "z": 5
        }
      }
    },
    "file": "game:itemtypes/toolhead/axehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-hammer.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType",
    "value": {
      "*-tinbronze": {
        "base": "toolsmith:item/parts/hammer/heads/normal"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/hammer/heads/normal"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/hammer/heads/normal"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/hammer/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/hammer/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/hammer/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/hammer/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/hammer/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/hammer/heads/simple"
      }
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/textures",
    "value": {
      "material": {
        "base": "block/metal/ingot/{metal}"
      }
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/shape",
    "value": {
      "base": "toolsmith:item/parts/hammer/heads/simple"
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/groundTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "origin": {
        "x": 1,
        "y": 0,
        "z": 0.5
      },
      "scale": 4.5
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": -0.5,
        "y": -0.12,
        "z": 0
      },
      "rotation": {
        "x": 60,
        "y": -70,
        "z": -50
      },
      "scale": 1.2
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransform",
    "value": {
      "translation": {
        "x": 4,
        "y": 1,
        "z": 0
      },
      "rotation": {
        "x": -60,
        "y": -115,
        "z": -160
      },
      "origin": {
        "x": 0.84,
        "y": 0,
        "z": 0.5
      },
      "scale": 4.0
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onTongTransform",
    "value": {
      "translation": {
        "x": -0.5,
        "y": -0.52,
        "z": -0.3
      },
      "rotation": {
        "x": -80,
        "y": -25,
        "z": 80
      }
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "remove",
    "path": "/attributes/onMetalTongTransform",
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onMetalTongTransformByType": {
        "@*-(gold|silver|iron|meteoriciron|steel)": {
          "translation": {
            "x": -0.9,
            "y": -0.598,
            "z": -0.61
          },
          "rotation": {
            "x": -5,
            "y": 0,
            "z": 83
          },
          "origin": {
            "x": 0.8,
            "y": 0.5,
            "z": 0.5
          }
        },
        "@*-(tinbronze|bismuthbronze|blackbronze)": {
          "translation": {
            "x": -0.9,
            "y": -0.592,
            "z": -0.61
          },
          "rotation": {
            "x": -5,
            "y": 0,
            "z": 83
          },
          "origin": {
            "x": 0.8,
            "y": 0.5,
            "z": 0.5
          }
        },
        "*": {
          "translation": {
            "x": -0.87,
            "y": -0.598,
            "z": -0.62
          },
          "rotation": {
            "x": -5,
            "y": 0,
            "z": 83
          },
          "origin": {
            "x": 0.8,
            "y": 0.5,
            "z": 0.5
          }
        }
      }
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": -0.34,
        "y": 0.03,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 125,
        "z": 0
      },
      "origin": {
        "x": 0.8,
        "y": 0,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/groundStorageTransform",
    "value": {
      "translation": {
        "x": -0.3,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 35,
        "z": 0
      },
      "origin": {
        "x": 0.8,
        "y": 0.5,
        "z": 0.5
      }
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onshelfTransform": {
        "translation": {
          "x": 0.23,
          "y": 0.01,
          "z": 0.2
        },
        "rotation": {
          "x": 0,
          "y": 135,
          "z": 0
        }
      }
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transform",
    "value": {
      "translation": {
        "x": -0.3,
        "y": 0.01,
        "z": 0
      }
    },
    "file": "game:itemtypes/toolhead/hammerhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/groundTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0,
        "z": 0.5
      },
      "scale": 4.5
    },
    "file": "game:itemtypes/tool/hammer.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": -0.55,
        "y": -0.42,
        "z": 0.04
      },
      "rotation": {
        "x": 90,
        "y": 1,
        "z": 0
      },
      "scale": 1.2
    },
    "file": "game:itemtypes/tool/hammer.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -77,
        "y": 46,
        "z": 8
      },
      "origin": {
        "x": 0.7,
        "y": 0.5,
        "z": 0.41
      },
      "scale": 2.6
    },
    "file": "game:itemtypes/tool/hammer.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/toolrackTransform",
    "value": {
      "translation": {
        "x": -0.2,
        "y": 0.02,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 1,
        "z": -1
      },
      "origin": {
        "x": 0.5,
        "y": -0.9,
        "z": 0.5
      },
      "scale": 1.5
    },
    "file": "game:itemtypes/tool/hammer.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/groundStorageTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.04,
        "z": -0.05
      },
      "rotation": {
        "x": 26,
        "y": 88,
        "z": -95
      },
      "scale": 1.01
    },
    "file": "game:itemtypes/tool/hammer.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0.15
      },
      "rotation": {
        "x": 4,
        "y": 90,
        "z": 0
      }
    },
    "file": "game:itemtypes/tool/hammer.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-hoe.json`
```json
[
  {
    "op": "replace",
    "path": "/shapeByType",
    "value": {
      "*-copper": {
        "base": "toolsmith:item/parts/hoe/heads/simple"
      },
      "*-tinbronze": {
        "base": "toolsmith:item/parts/hoe/heads/simple"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/hoe/heads/simple"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/hoe/heads/simple"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/hoe/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/hoe/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/hoe/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/hoe/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/hoe/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/hoe/heads/nonmetal"
      }
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransformByType",
    "value": {
      "@*-(gold|silver|iron|meteoriciron|steel)": {
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -60,
          "y": -60,
          "z": -105
        },
        "origin": {
          "x": 0.95,
          "y": 0,
          "z": 0.33
        },
        "scale": 2.9
      },
      "@*-(copper|tinbronze|bismuthbronze|blackbronze)": {
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -60,
          "y": -60,
          "z": -125
        },
        "origin": {
          "x": 0.95,
          "y": 0,
          "z": 0.32
        },
        "scale": 3.3
      },
      "*": {
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -60,
          "y": -60,
          "z": -125
        },
        "origin": {
          "x": 0.95,
          "y": 0,
          "z": 0.38
        },
        "scale": 3.76
      }
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": -0.3,
        "y": 0.04,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 90,
        "z": 0
      },
      "origin": {
        "x": 0.9,
        "y": 0,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": -0.5,
        "y": -1,
        "z": -0.02
      },
      "rotation": {
        "x": 96,
        "y": -85,
        "z": -8
      },
      "origin": {
        "x": 0.5,
        "y": 0.5,
        "z": 0.5
      },
      "scale": 1
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onshelfTransform": {
        "translation": {
          "x": -0.3,
          "y": 0,
          "z": 0.3
        },
        "rotation": {
          "x": 0,
          "y": 35,
          "z": 0
        }
      }
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transformByType",
    "value": {
      "@*-(copper|tinbronze|bismuthbronze|blackbronze|gold|silver|iron|meteoriciron|steel)": {
        "translation": {
          "x": -0.43,
          "y": 0.02,
          "z": 0.12
        }
      },
      "*": {
        "translation": {
          "x": -0.45,
          "y": 0,
          "z": 0.06
        }
      }
    },
    "file": "game:itemtypes/toolhead/hoehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-knife.json`
```json
[
  {
    "op": "replace",
    "path": "/shapeByType",
    "value": {
      "*-copper": {
        "base": "toolsmith:item/parts/knife/heads/simple"
      },
      "*-tinbronze": {
        "base": "toolsmith:item/parts/knife/heads/simple"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/knife/heads/simple"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/knife/heads/simple"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/knife/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/knife/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/knife/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/knife/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/knife/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/knife/heads/nonmetal"
      }
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransformByType",
    "value": {
      "@knifeblade-(chert|flint|granite|andesite|basalt|obsidian|peridotite)": {
        "rotate": false,
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -8,
          "y": -152,
          "z": 142
        },
        "origin": {
          "x": 0.49,
          "y": 0.06,
          "z": 0.43
        },
        "scale": 3.4
      },
      "*": {
        "translation": {
          "x": 3,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -8,
          "y": -152,
          "z": 142
        },
        "origin": {
          "x": 0.53,
          "y": 0.05,
          "z": 0.36
        },
        "scale": 3.53
      }
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.05,
        "z": 0.1
      },
      "rotation": {
        "x": -75,
        "y": 0,
        "z": 135
      },
      "origin": {
        "x": 0.5,
        "y": 0.1,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onshelfTransform": {
        "translation": {
          "x": -0.08,
          "y": -0.09,
          "z": 0.04
        },
        "rotation": {
          "x": -90,
          "y": 0,
          "z": 45
        },
        "origin": {
          "x": 0.5,
          "y": 0.1,
          "z": 0.5
        }
      }
    },
    "file": "game:itemtypes/toolhead/knifeblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-pickaxe.json`
```json
[
  {
    "op": "replace",
    "path": "/shapeByType",
    "value": {
      "*-tinbronze": {
        "base": "toolsmith:item/parts/pickaxe/heads/normal"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/pickaxe/heads/normal"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/pickaxe/heads/normal"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/pickaxe/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/pickaxe/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/pickaxe/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/pickaxe/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/pickaxe/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/pickaxe/heads/simple"
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/texturesByType",
    "value": {
      "*": {
        "material": {
          "base": "block/metal/ingot/{metal}"
        }
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/groundTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "origin": {
        "x": 1,
        "y": 0,
        "z": 0.5
      },
      "scale": 4.17
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": 180,
        "y": 85,
        "z": 0
      },
      "origin": {
        "x": -1.5,
        "y": 0.05,
        "z": 2
      },
      "scale": 0.8
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -90,
        "y": 45,
        "z": -35
      },
      "origin": {
        "x": 0.85,
        "y": 0,
        "z": 0.45
      },
      "scale": 2.0
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onTongTransform",
    "value": {
      "translation": {
        "x": -0.75,
        "y": 0.22,
        "z": 0.94
      },
      "rotation": {
        "x": -90,
        "y": -25,
        "z": 90
      },
      "origin": {
        "x": 0,
        "y": 0,
        "z": 0
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": -0.42,
        "y": 0.06,
        "z": -0.04
      },
      "rotation": {
        "x": 15,
        "y": 45,
        "z": 0
      },
      "origin": {
        "x": 1,
        "y": 0,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transform",
    "value": {
      "translation": {
        "x": -0.45,
        "y": -0.12,
        "z": 0.12
      },
      "origin": {
        "x": 1,
        "y": 0.5,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onshelfTransform": {
        "translation": {
          "x": -0.42,
          "y": -0.135,
          "z": 0.1
        },
        "rotation": {
          "x": 0,
          "y": -45,
          "z": 0
        },
        "origin": {
          "x": 1,
          "y": 0.5,
          "z": 0.5
        },
        "scale": 0.7
      }
    },
    "file": "game:itemtypes/toolhead/pickaxehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-prospectingpick.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType",
    "value": {
      "*-tinbronze": {
        "base": "toolsmith:item/parts/prospectingpick/heads/simple"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/prospectingpick/heads/simple"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/prospectingpick/heads/simple"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/prospectingpick/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/prospectingpick/heads/simple"
      }
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/textures",
    "value": {
      "material": {
        "base": "block/metal/ingot/{metal}"
      }
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/shape",
    "value": {
      "base": "toolsmith:item/parts/prospectingpick/heads/simple"
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onMetalTongTransform",
    "value": {
      "translation": {
        "x": -1.54,
        "y": -0.075,
        "z": -0.54
      },
      "rotation": {
        "x": 176,
        "y": 0,
        "z": 8
      },
      "origin": {
        "x": 1,
        "y": 0,
        "z": 0.5
      }
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": -0.2,
        "y": -0.06,
        "z": 0.35
      },
      "rotation": {
        "x": 15,
        "y": 45,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transform",
    "value": {
      "translation": {
        "x": -0.48,
        "y": 0,
        "z": 0.1
      }
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onshelfTransform": {
        "translation": {
          "x": -0.25,
          "y": 0,
          "z": 0.42
        },
        "rotation": {
          "x": 0,
          "y": -45,
          "z": 0
        }
      }
    },
    "file": "game:itemtypes/toolhead/prospectingpickhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-saw.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType",
    "value": {
      "*-tinbronze": {
        "base": "toolsmith:item/parts/saw/heads/simple"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/saw/heads/simple"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/saw/heads/simple"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/saw/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/saw/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/saw/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/saw/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/saw/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/saw/heads/simple"
      }
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/textures",
    "value": {
      "material": {
        "base": "block/metal/ingot/{metal}"
      }
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/shape",
    "value": {
      "base": "toolsmith:item/parts/saw/heads/simple"
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/groundTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0,
        "z": 0.5
      },
      "scale": 3.35
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -29,
        "y": -73,
        "z": -180
      },
      "origin": {
        "x": 0.5,
        "y": 0.05,
        "z": 0.64
      },
      "scale": 3.5
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": -0.9,
        "y": -0.45,
        "z": -0.05
      },
      "rotation": {
        "x": 90,
        "y": -174,
        "z": 0
      },
      "scale": 1
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onTongTransform",
    "value": {
      "translation": {
        "x": -1.1,
        "y": -0.7,
        "z": -0.95
      },
      "rotation": {
        "x": 168,
        "y": -98,
        "z": 105
      }
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onMetalTongTransform",
    "value": {
      "translation": {
        "x": -1.04,
        "y": -0.18,
        "z": -0.75
      },
      "rotation": {
        "x": 0,
        "y": 89,
        "z": 4
      },
      "origin": {
        "x": 0.5,
        "y": 0.5,
        "z": 0.6
      }
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": -0.12,
        "y": -0.15,
        "z": 0.125
      },
      "rotation": {
        "x": 15,
        "y": 110,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0.56,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transform",
    "value": {
      "translation": {
        "x": -0.1,
        "y": -0.05,
        "z": 0.05
      },
      "rotation": {
        "x": 0,
        "y": 90,
        "z": 0
      }
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onshelfTransform": {
        "translation": {
          "x": -0.05,
          "y": -0.05,
          "z": -0.05
        },
        "rotation": {
          "x": 0,
          "y": 45,
          "z": 0
        }
      }
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/groundStorageTransform",
    "value": {
      "translation": {
        "x": 0.01,
        "y": -0.04,
        "z": -0.1
      },
      "rotation": {
        "x": 0,
        "y": -54,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0.5,
        "z": 0.6
      }
    },
    "file": "game:itemtypes/toolhead/sawblade.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -77,
        "y": 47,
        "z": -151
      },
      "origin": {
        "x": 0.45,
        "y": 0,
        "z": 0.425
      },
      "scale": 1.9
    },
    "file": "game:itemtypes/tool/saw.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/groundTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0.3,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0.45,
        "z": 0.5
      },
      "scale": 3.6
    },
    "file": "game:itemtypes/tool/saw.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": -0.65,
        "y": -0.62,
        "z": -0.05
      },
      "rotation": {
        "x": 90,
        "y": -174,
        "z": 0
      },
      "scale": 1
    },
    "file": "game:itemtypes/tool/saw.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/groundStorageTransform",
    "value": {
      "translation": {
        "x": 0.03,
        "y": 0,
        "z": -0.05
      },
      "rotation": {
        "x": 26,
        "y": 90,
        "z": -90
      },
      "scale": 0.88
    },
    "file": "game:itemtypes/tool/saw.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/toolrackTransform",
    "value": {
      "translation": {
        "x": -0.15,
        "y": 0.65,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 1,
        "z": -1
      },
      "scale": 1.35
    },
    "file": "game:itemtypes/tool/saw.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-scythe.json`
```json
[
  {
    "op": "addmerge",
    "path": "/shapeByType",
    "value": {
      "*-tinbronze": {
        "base": "toolsmith:item/parts/scythe/heads/simple"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/scythe/heads/simple"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/scythe/heads/simple"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/scythe/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/scythe/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/scythe/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/scythe/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/scythe/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/scythe/heads/simple"
      }
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/textures",
    "value": {
      "material": {
        "base": "block/metal/ingot/{metal}"
      }
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/shape",
    "value": {
      "base": "toolsmith:item/parts/scythe/heads/simple"
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -74,
        "y": 35,
        "z": 12
      },
      "origin": {
        "x": 1.5,
        "y": 0,
        "z": 0.1
      },
      "scale": 1.6
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransform",
    "value": {
      "translation": {
        "x": -2,
        "y": -0.21,
        "z": -0.73
      },
      "rotation": {
        "x": 102,
        "y": -80,
        "z": 1
      },
      "origin": {
        "x": 1.4,
        "y": 0,
        "z": 0.5
      },
      "scale": 0.7799
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": -0.97,
        "y": -0.17,
        "z": 0.45
      },
      "rotation": {
        "x": 15,
        "y": 30,
        "z": 0
      },
      "origin": {
        "x": 1.5,
        "y": 0.5,
        "z": 0.3
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onTongTransform",
    "value": {
      "translation": {
        "x": 0.15,
        "y": -1,
        "z": -0.97
      },
      "rotation": {
        "x": -90,
        "y": 0,
        "z": 0
      }
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onMetalTongTransform",
    "value": {
      "translation": {
        "x": -1.92,
        "y": -0.166,
        "z": 0
      },
      "rotation": {
        "x": 0,
        "y": 0,
        "z": -3
      },
      "origin": {
        "x": 1.5,
        "y": 0,
        "z": 0
      }
    },
    "file": "game:itemtypes/toolhead/scythehead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/partshapes/vanilla-shovel.json`
```json
[
  {
    "op": "replace",
    "path": "/shapeByType",
    "value": {
      "*-copper": {
        "base": "toolsmith:item/parts/shovel/heads/simple"
      },
      "*-tinbronze": {
        "base": "toolsmith:item/parts/shovel/heads/simple"
      },
      "*-bismuthbronze": {
        "base": "toolsmith:item/parts/shovel/heads/simple"
      },
      "*-blackbronze": {
        "base": "toolsmith:item/parts/shovel/heads/simple"
      },
      "*-gold": {
        "base": "toolsmith:item/parts/shovel/heads/advanced"
      },
      "*-silver": {
        "base": "toolsmith:item/parts/shovel/heads/advanced"
      },
      "*-iron": {
        "base": "toolsmith:item/parts/shovel/heads/advanced"
      },
      "*-steel": {
        "base": "toolsmith:item/parts/shovel/heads/advanced"
      },
      "*-meteoriciron": {
        "base": "toolsmith:item/parts/shovel/heads/advanced"
      },
      "*": {
        "base": "toolsmith:item/parts/shovel/heads/nonmetal"
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/guiTransformByType",
    "value": {
      "@shovelhead-(chert|flint|granite|andesite|basalt|obsidian|peridotite)": {
        "rotate": false,
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": -45,
          "y": -134,
          "z": -180
        },
        "origin": {
          "x": 1.27,
          "y": 0.15,
          "z": 0.5
        },
        "scale": 2.75
      },
      "*": {
        "translation": {
          "x": 0,
          "y": 0,
          "z": 0
        },
        "rotation": {
          "x": 141,
          "y": 33,
          "z": -38
        },
        "origin": {
          "x": 1.22,
          "y": 0,
          "z": 0.6
        },
        "scale": 2.45
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/tpHandTransformByType",
    "value": {
      "@shovelhead-(copper|tinbronze|bismuthbronze|blackbronze|iron|meteoriciron|steel|gold|silver)": {
        "translation": {
          "x": -0.2,
          "y": -0.4,
          "z": -0.15
        },
        "rotation": {
          "x": 90,
          "y": 20,
          "z": 0
        },
        "scale": 0.85
      },
      "*": {
        "translation": {
          "x": 0,
          "y": -0.4,
          "z": -0.1
        },
        "rotation": {
          "x": 90,
          "y": 20,
          "z": 0
        },
        "scale": 0.85
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onTongTransform",
    "value": {
      "translation": {
        "x": -0.3,
        "y": -0.68,
        "z": -0.98
      },
      "rotation": {
        "x": -90,
        "y": 0,
        "z": 0
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/onMetalTongTransform",
    "value": {
      "translation": {
        "x": -1.15,
        "y": -0.23,
        "z": -1.3
      },
      "rotation": {
        "x": -2,
        "y": 90,
        "z": 0
      },
      "origin": {
        "x": 0.5,
        "y": 0,
        "z": 0.5
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/inForgeTransform",
    "value": {
      "translation": {
        "x": 0,
        "y": -0.01,
        "z": 0.4
      },
      "rotation": {
        "x": 0,
        "y": 120,
        "z": 0
      },
      "origin": {
        "x": 0.7,
        "y": 0,
        "z": 0.5
      },
      "scale": 0.7
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "addmerge",
    "path": "/attributes",
    "value": {
      "onshelfTransform": {
        "translation": {
          "x": -0.76,
          "y": -0.05,
          "z": 0
        },
        "rotation": {
          "x": 0,
          "y": 20,
          "z": 0
        },
        "origin": {
          "x": 1.2,
          "y": 0.5,
          "z": 0.5
        },
        "scale": 0.9
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/displayable/shelf/transformByType",
    "value": {
      "@*-(copper|tinbronze|bismuthbronze|blackbronze|gold|silver|iron|meteoriciron|steel)": {
        "translation": {
          "x": -0.71,
          "y": -0.1,
          "z": 0.05
        },
        "rotation": {
          "x": 0,
          "y": 90,
          "z": 0
        },
        "origin": {
          "x": 1.2,
          "y": 0.5,
          "z": 0.5
        },
        "scale": 0.8
      },
      "*": {
        "translation": {
          "x": -0.72,
          "y": -0.11,
          "z": 0.1
        },
        "rotation": {
          "x": 0,
          "y": 90,
          "z": 0
        },
        "origin": {
          "x": 1.2,
          "y": 0.5,
          "z": 0.5
        },
        "scale": 0.8
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/groundStorageTransformByType",
    "value": {
      "@*-(copper|tinbronze|bismuthbronze|blackbronze|gold|silver|iron|meteoriciron|steel)": {
        "translation": {
          "x": -0.45,
          "y": -0.03,
          "z": 0.45
        },
        "rotation": {
          "x": 0,
          "y": 45,
          "z": -3
        },
        "scale": 0.9
      },
      "*": {
        "translation": {
          "x": -0.75,
          "y": -0.02,
          "z": 0.1
        },
        "rotation": {
          "x": 0,
          "y": 44,
          "z": 0
        },
        "origin": {
          "x": 1.2,
          "y": 0.5,
          "z": 0.5
        }
      }
    },
    "file": "game:itemtypes/toolhead/shovelhead.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  },
  {
    "op": "replace",
    "path": "/attributes/groundStorageTransformByType",
    "value": {
      "@shovel-(copper|tinbronze|bismuthbronze|blackbronze|gold|silver|iron|meteoriciron|steel)": {
        "translation": {
          "x": 0,
          "y": 0.5,
          "z": -0.15
        },
        "rotation": {
          "x": 13,
          "y": 88,
          "z": -87
        },
        "scale": 0.88
      },
      "*": {
        "translation": {
          "x": 0,
          "y": 0.5,
          "z": -0.15
        },
        "rotation": {
          "x": 13,
          "y": 88,
          "z": -87
        },
        "scale": 0.88
      }
    },
    "file": "game:itemtypes/tool/shovel.json",
    "condition": {
      "when": "Toolsmith_MultiPartRendering",
      "isValue": "true"
    }
  }
]
```
### `assets/toolsmith/patches/toolshapes/vanilla-shears.json`
```json
[
  {
    "op": "replace",
    "path": "/shape",
    "value": {
      "base": "toolsmith:item/tools/shears"
    },
    "file": "game:itemtypes/tool/shears.json"
  },
  {
    "op": "replace",
    "path": "/guiTransform",
    "value": {
      "translation": {
        "x": 0.5,
        "y": 0,
        "z": 0
      },
      "rotation": {
        "x": -89,
        "y": 45,
        "z": 33
      },
      "origin": {
        "x": 0.9,
        "y": 0.4,
        "z": 0.48
      },
      "scale": 2.5
    },
    "file": "game:itemtypes/tool/shears.json"
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/bindings/bindings-ancienttools.json`
```json
[
  {
    "id": "fabricstrips-atleather",
    "bindingStatTag": "leather",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/bindings/bindings-butchery.json`
```json
[
  {
    "id": "sinew-dry",
    "bindingStatTag": "rope",
    "bindingShapePath": "string",
    "bindingTextureOverride": "butchering:item/resource/sinew"
  },
  {
    "id": "glueportion-sinew-cold",
    "bindingStatTag": "glue",
    "isLiquid": true,
    "litersUsed": 0.5,
    "bindingShapePath": "",
    "bindingTextureOverride": ""
  },
  {
    "id": "glueportion-hide-hot",
    "bindingStatTag": "glue",
    "isLiquid": true,
    "litersUsed": 0.5,
    "bindingShapePath": "",
    "bindingTextureOverride": ""
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/bindings/bindings-primitivesurvival.json`
```json
[
  {
    "id": "cordage",
    "bindingStatTag": "rope",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/bindings/bindings-stringsense.json`
```json
[
  {
    "id": "cord-bamboo",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:bamboo/cord"
  },
  {
    "id": "cord-bark",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:bark/cord"
  },
  {
    "id": "cord-coconut",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:coconut/cord"
  },
  {
    "id": "cord-flax",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:flax/cord"
  },
  {
    "id": "cord-mixed",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:mixed/cord"
  },
  {
    "id": "cord-nzflax",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:nzflax/cord"
  },
  {
    "id": "cord-palm",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:palm/cord"
  },
  {
    "id": "cord-papyrus",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:papyrus/cord"
  },
  {
    "id": "cord-porcupine",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:porcupine/cord"
  },
  {
    "id": "cord-reed",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:reed/cord"
  },
  {
    "id": "cord-sinew",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:sinew/cord"
  },
  {
    "id": "cord-straw",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:straw/cord"
  },
  {
    "id": "cord-vine",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:vine/cord"
  },
  {
    "id": "cord-larch",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:larch/cord"
  },
  {
    "id": "cord-tuja",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:tuja/cord"
  },
  {
    "id": "cord-honeylocust",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:honeylocust/cord"
  },
  {
    "id": "cord-birch",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:birch/cord"
  },
  {
    "id": "cord-brideinwhite",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:brideinwhite/cord"
  },
  {
    "id": "cord-alder",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:alder/cord"
  },
  {
    "id": "cord-bearnut",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:bearnut/cord"
  },
  {
    "id": "cord-willow",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:willow/cord"
  },
  {
    "id": "cord-redwillow",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:redwillow/cord"
  },
  {
    "id": "cord-poplar",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:poplar/cord"
  },
  {
    "id": "cord-aspen",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:aspen/cord"
  },
  {
    "id": "cord-eucalyptus",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:eucalyptus/cord"
  },
  {
    "id": "cord-kapok",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:kapok/cord"
  },
  {
    "id": "cord-sapele",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": "stringsense:sapele/cord"
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/bindings/bindings-tailorsdelight.json`
```json
[
  {
    "id": "twine-mordant",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/mordant"
  },
  {
    "id": "twine-black",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/black"
  },
  {
    "id": "twine-blue",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/blue"
  },
  {
    "id": "twine-brown",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/brown"
  },
  {
    "id": "twine-gray",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/gray"
  },
  {
    "id": "twine-green",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/green"
  },
  {
    "id": "twine-orange",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/orange"
  },
  {
    "id": "twine-pink",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/pink"
  },
  {
    "id": "twine-purple",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/purple"
  },
  {
    "id": "twine-red",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/red"
  },
  {
    "id": "twine-white",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/white"
  },
  {
    "id": "twine-yellow",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/yellow"
  },
  {
    "id": "twine-darkblue",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkblue"
  },
  {
    "id": "twine-darkbrown",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkbrown"
  },
  {
    "id": "twine-darkgreen",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkgreen"
  },
  {
    "id": "twine-darkred",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkred"
  },
  {
    "id": "leatherstrips-plain",
    "bindingStatTag": "leather",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/bindings/bindings-vanilla.json`
```json
[
  {
    "id": "drygrass",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "cattailtops",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "papyrustops",
    "bindingStatTag": "reeds",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "flaxtwine",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "fabricstrips-rawhide",
    "bindingStatTag": "hide",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "rope",
    "bindingStatTag": "rope",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "fabricstrips-cloth",
    "bindingStatTag": "cloth",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "fabricstrips-leather",
    "bindingStatTag": "leather",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "fabricstrips-sturdy",
    "bindingStatTag": "sturdy",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  },
  {
    "id": "glueportion-pitch-hot",
    "bindingStatTag": "glue",
    "isLiquid": true,
    "litersUsed": 0.5,
    "bindingShapePath": "",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-copper",
    "bindingStatTag": "coppernails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-tinbronze",
    "bindingStatTag": "tinbronzenails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-bismuthbronze",
    "bindingStatTag": "bismuthbronzenails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-blackbronze",
    "bindingStatTag": "blackbronzenails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-iron",
    "bindingStatTag": "ironnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-cupronickel",
    "bindingStatTag": "cupronickelnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-meteoriciron",
    "bindingStatTag": "meteoricironnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metalnailsandstrips-steel",
    "bindingStatTag": "steelnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  },
  {
    "id": "metal-parts",
    "bindingStatTag": "cupronickelnails",
    "bindingShapePath": "metal",
    "bindingTextureOverride": ""
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/bindings/bindings-wool.json`
```json
[
  {
    "id": "twine-wool-mordant",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/mordant"
  },
  {
    "id": "twine-wool-plain",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/plain"
  },
  {
    "id": "twine-wool-black",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/black"
  },
  {
    "id": "twine-wool-blue",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/blue"
  },
  {
    "id": "twine-wool-brown",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/brown"
  },
  {
    "id": "twine-wool-gray",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/gray"
  },
  {
    "id": "twine-wool-green",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/green"
  },
  {
    "id": "twine-wool-orange",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/orange"
  },
  {
    "id": "twine-wool-pink",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/pink"
  },
  {
    "id": "twine-wool-purple",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/purple"
  },
  {
    "id": "twine-wool-red",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/red"
  },
  {
    "id": "twine-wool-white",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/white"
  },
  {
    "id": "twine-wool-yellow",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "game:block/cloth/linen/yellow"
  },
  {
    "id": "twine-wool-darkblue",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "wool:block/cloth/wool/darkblue"
  },
  {
    "id": "twine-wool-darkbrown",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "wool:block/cloth/wool/darkbrown"
  },
  {
    "id": "twine-wool-darkgreen",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "wool:block/cloth/wool/darkgreen"
  },
  {
    "id": "twine-wool-darkred",
    "bindingStatTag": "twine",
    "bindingShapePath": "string",
    "bindingTextureOverride": "wool:block/cloth/wool/darkred"
  },
  {
    "id": "fabricstrips-wool",
    "bindingStatTag": "cloth",
    "bindingShapePath": "string",
    "bindingTextureOverride": ""
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/grips/grips-ancienttools.json`
```json
[
  {
    "id": "leather-normal-acacia",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/acacia"
  },
  {
    "id": "leather-normal-aged",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/aged"
  },
  {
    "id": "leather-normal-baldcypress",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/baldcypress"
  },
  {
    "id": "leather-normal-birch",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/birch"
  },
  {
    "id": "leather-normal-cypress",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/cypress"
  },
  {
    "id": "leather-normal-ebony",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/ebony"
  },
  {
    "id": "leather-normal-kapok",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/kapok"
  },
  {
    "id": "leather-normal-larch",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/larch"
  },
  {
    "id": "leather-normal-maple",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/maple"
  },
  {
    "id": "leather-normal-pine",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/pine"
  },
  {
    "id": "leather-normal-purpleheart",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/purpleheart"
  },
  {
    "id": "leather-normal-redwood",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/redwood"
  },
  {
    "id": "leather-normal-walnut",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/leather/walnut"
  },
  {
    "id": "buckskin",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "ancienttools:item/resource/buckskin"
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/grips/grips-tailorsdelight.json`
```json
[
  {
    "id": "cloth-darkblue",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkblue"
  },
  {
    "id": "cloth-darkbrown",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkbrown"
  },
  {
    "id": "cloth-darkgreen",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkgreen"
  },
  {
    "id": "cloth-darkred",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkred"
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/grips/grips-vanilla.json`
```json
[
  {
    "id": "flaxtwine",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": ""
  },
  {
    "id": "linen-diamond-down",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/diamond1"
  },
  {
    "id": "linen-normal-down",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/normal1"
  },
  {
    "id": "linen-offset-down",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/offset1"
  },
  {
    "id": "linen-square-down",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/square1"
  },
  {
    "id": "cloth-plain",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/normal1"
  },
  {
    "id": "cloth-mordant",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/mordant"
  },
  {
    "id": "cloth-blue",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/blue"
  },
  {
    "id": "cloth-red",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/red"
  },
  {
    "id": "cloth-yellow",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/yellow"
  },
  {
    "id": "cloth-green",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/green"
  },
  {
    "id": "cloth-purple",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/purple"
  },
  {
    "id": "cloth-pink",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/pink"
  },
  {
    "id": "cloth-orange",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/orange"
  },
  {
    "id": "cloth-brown",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/brown"
  },
  {
    "id": "cloth-gray",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/gray"
  },
  {
    "id": "cloth-black",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/black"
  },
  {
    "id": "cloth-white",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/white"
  },
  {
    "id": "leather-normal-plain",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": ""
  },
  {
    "id": "leather-normal-orange",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/orange"
  },
  {
    "id": "leather-normal-black",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/black"
  },
  {
    "id": "leather-normal-red",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/red"
  },
  {
    "id": "leather-normal-blue",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/blue"
  },
  {
    "id": "leather-normal-purple",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/purple"
  },
  {
    "id": "leather-normal-pink",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/pink"
  },
  {
    "id": "leather-normal-white",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/white"
  },
  {
    "id": "leather-normal-yellow",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/yellow"
  },
  {
    "id": "leather-normal-gray",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/gray"
  },
  {
    "id": "leather-normal-green",
    "gripStatTag": "leather",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/leather/green"
  },
  {
    "id": "leather-sturdy-plain",
    "gripStatTag": "sturdy",
    "gripShapePath": "fabric",
    "gripTextureOverride": ""
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/grips/grips-wool.json`
```json
[
  {
    "id": "twine-wool-mordant",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/mordant"
  },
  {
    "id": "twine-wool-plain",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/normal1"
  },
  {
    "id": "twine-wool-black",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/black"
  },
  {
    "id": "twine-wool-blue",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/blue"
  },
  {
    "id": "twine-wool-brown",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/brown"
  },
  {
    "id": "twine-wool-gray",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/gray"
  },
  {
    "id": "twine-wool-green",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/green"
  },
  {
    "id": "twine-wool-orange",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/orange"
  },
  {
    "id": "twine-wool-pink",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/pink"
  },
  {
    "id": "twine-wool-purple",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/purple"
  },
  {
    "id": "twine-wool-red",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/red"
  },
  {
    "id": "twine-wool-white",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/white"
  },
  {
    "id": "twine-wool-yellow",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/yellow"
  },
  {
    "id": "twine-wool-darkblue",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkblue"
  },
  {
    "id": "twine-wool-darkbrown",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkbrown"
  },
  {
    "id": "twine-wool-darkgreen",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkgreen"
  },
  {
    "id": "twine-wool-darkred",
    "gripStatTag": "twine",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkred"
  },
  {
    "id": "cloth-wool-mordant",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "game:block/cloth/linen/mordant"
  },
  {
    "id": "cloth-wool-plain",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/plain"
  },
  {
    "id": "cloth-wool-black",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/black"
  },
  {
    "id": "cloth-wool-blue",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/blue"
  },
  {
    "id": "cloth-wool-brown",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/brown"
  },
  {
    "id": "cloth-wool-gray",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/gray"
  },
  {
    "id": "cloth-wool-green",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/green"
  },
  {
    "id": "cloth-wool-orange",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/orange"
  },
  {
    "id": "cloth-wool-pink",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/pink"
  },
  {
    "id": "cloth-wool-purple",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/purple"
  },
  {
    "id": "cloth-wool-red",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/red"
  },
  {
    "id": "cloth-wool-white",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/white"
  },
  {
    "id": "cloth-wool-yellow",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/yellow"
  },
  {
    "id": "cloth-wool-darkblue",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkblue"
  },
  {
    "id": "cloth-wool-darkbrown",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkbrown"
  },
  {
    "id": "cloth-wool-darkgreen",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkgreen"
  },
  {
    "id": "cloth-wool-darkred",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkred"
  },
  {
    "id": "wool-plain",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/plain"
  },
  {
    "id": "wool-black",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/black"
  },
  {
    "id": "wool-blue",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/blue"
  },
  {
    "id": "wool-brown",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/brown"
  },
  {
    "id": "wool-gray",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/gray"
  },
  {
    "id": "wool-green",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/green"
  },
  {
    "id": "wool-orange",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/orange"
  },
  {
    "id": "wool-pink",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/pink"
  },
  {
    "id": "wool-purple",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/purple"
  },
  {
    "id": "wool-red",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/red"
  },
  {
    "id": "wool-white",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/white"
  },
  {
    "id": "wool-yellow",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/linen/yellow"
  },
  {
    "id": "wool-darkblue",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkblue"
  },
  {
    "id": "wool-darkbrown",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkbrown"
  },
  {
    "id": "wool-darkgreen",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkgreen"
  },
  {
    "id": "wool-darkred",
    "gripStatTag": "cloth",
    "gripShapePath": "fabric",
    "gripTextureOverride": "wool:block/cloth/wool/darkred"
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/handles/handles-vanilla.json`
```json
[
  {
    "id": "stick",
    "handleStatTag": "stick",
    "canHaveGrip": false,
    "handleShapePath": "toolsmith:shapes/item/parts/handles/stick/handle",
    "canBeTreated": false,
    "dryingTimeMult": 1.0
  },
  {
    "id": "bone",
    "handleStatTag": "bone",
    "canHaveGrip": true,
    "handleShapePath": "toolsmith:shapes/item/parts/handles/bone/handle",
    "canBeTreated": false,
    "dryingTimeMult": 1.0
  },
  {
    "id": "crudehandle",
    "handleStatTag": "crude",
    "canHaveGrip": true,
    "handleShapePath": "toolsmith:shapes/item/parts/handles/crude/handle",
    "canBeTreated": false,
    "dryingTimeMult": 1.0
  },
  {
    "id": "handle",
    "handleStatTag": "handle",
    "canHaveGrip": true,
    "handleShapePath": "toolsmith:shapes/item/parts/handles/regular/handle",
    "canBeTreated": true,
    "dryingTimeMult": 1.0
  },
  {
    "id": "carpentedhandle",
    "handleStatTag": "professional",
    "canHaveGrip": true,
    "handleShapePath": "toolsmith:shapes/item/parts/handles/fine/handle",
    "canBeTreated": true,
    "dryingTimeMult": 2.0
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/treatments/treatments-expandedfoods.json`
```json
[
  {
    "id": "foodoilportion-flax",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.1
  },
  {
    "id": "foodoilportion-seed",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.1
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/treatments/treatments-oilsandmore.json`
```json
[
  {
    "id": "plantoil-flax",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.1
  },
  {
    "id": "plantoil-walnut",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.1
  },
  {
    "id": "woodfinish-flax",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.05
  },
  {
    "id": "woodfinish-walnut",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.05
  }
]
```
### `assets/toolsmith/config/toolsmith/parts/treatments/treatments-vanilla.json`
```json
[
  {
    "id": "fat",
    "treatmentStatTag": "fat",
    "dryingHours": 12,
    "isLiquid": false,
    "litersUsed": 0.0
  },
  {
    "id": "fat-rendered",
    "treatmentStatTag": "fat",
    "dryingHours": 12,
    "isLiquid": false,
    "litersUsed": 0.0
  },
  {
    "id": "beeswax",
    "treatmentStatTag": "wax",
    "dryingHours": 12,
    "isLiquid": false,
    "litersUsed": 0.0
  },
  {
    "id": "oilportion-flax",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.1
  },
  {
    "id": "oilportion-olive",
    "treatmentStatTag": "oil",
    "dryingHours": 24,
    "isLiquid": true,
    "litersUsed": 0.1
  }
]
```
### `assets/toolsmith/config/toolsmith/regex/bluntheadedtools/bluntheadedtools-ageofgrowing.json`
```json
[
  "creaser",
  "flail"
]
```
### `assets/toolsmith/config/toolsmith/regex/bluntheadedtools/bluntheadedtools-ancienttools.json`
```json
[
  "mallet"
]
```
### `assets/toolsmith/config/toolsmith/regex/bluntheadedtools/bluntheadedtools-base.json`
```json
[
  "hammer",
  "tongs",
  "wrench"
]
```
### `assets/toolsmith/config/toolsmith/regex/bluntheadedtools/bluntheadedtools-chiseltools.json`
```json
[
  "handwedge",
  "laddermaker",
  "paintbrush",
  "pantograph",
  "pathmaker"
]
```
### `assets/toolsmith/config/toolsmith/regex/bluntheadedtools/bluntheadedtools-expandedfoods.json`
```json
[
  "rollingpin"
]
```
### `assets/toolsmith/config/toolsmith/regex/bluntheadedtools/bluntheadedtools-spyglass.json`
```json
[
  "spyglass"
]
```
### `assets/toolsmith/config/toolsmith/regex/bluntheadedtools/bluntheadedtools-stonequarry.json`
```json
[
  "rubblehammer"
]
```
### `assets/toolsmith/config/toolsmith/regex/partblacklist/partblacklist-admintools.json`
```json
[
  "wfradmin"
]
```
### `assets/toolsmith/config/toolsmith/regex/partblacklist/partblacklist-armory.json`
```json
[
  "armory"
]
```
### `assets/toolsmith/config/toolsmith/regex/partblacklist/partblacklist-base.json`
```json
[
  "helve",
  "chiseledblock",
  "stickslayer",
  "scrap",
  "ruined",
  "chiseled",
  "wooden",
  "block"
]
```
### `assets/toolsmith/config/toolsmith/regex/partblacklist/partblacklist-molds.json`
```json
[
  "chiselmold",
  "wrenchmold",
  "knifemold"
]
```
### `assets/toolsmith/config/toolsmith/regex/partblacklist/partblacklist-saws.json`
```json
[
  "sawmill",
  "sawbuck",
  "sawhorse",
  "sawdust"
]
```
### `assets/toolsmith/config/toolsmith/regex/partblacklist/partblacklist-tailorsdelight.json`
```json
[
  "awl-bone",
  "awl-horn",
  "awl-flint",
  "awl-obsidian"
]
```
### `assets/toolsmith/config/toolsmith/regex/singleparttools/singleparttools-ancienttools.json`
```json
[
  "wedge"
]
```
### `assets/toolsmith/config/toolsmith/regex/singleparttools/singleparttools-artofgrowing.json`
```json
[
  "creaser",
  "flail"
]
```
### `assets/toolsmith/config/toolsmith/regex/singleparttools/singleparttools-base.json`
```json
[
  "chisel",
  "shears",
  "tongs",
  "wrench"
]
```
### `assets/toolsmith/config/toolsmith/regex/singleparttools/singleparttools-canjewelry.json`
```json
[
  "cangemchisel"
]
```
### `assets/toolsmith/config/toolsmith/regex/singleparttools/singleparttools-chiseltools.json`
```json
[
  "truechisel",
  "handplaner",
  "handwedge",
  "laddermaker",
  "paintbrush",
  "paintscraper",
  "pantograph",
  "pathmaker"
]
```
### `assets/toolsmith/config/toolsmith/regex/singleparttools/singleparttools-expandedfoods.json`
```json
[
  "rollingpin"
]
```
### `assets/toolsmith/config/toolsmith/regex/singleparttools/singleparttools-spyglass.json`
```json
[
  "spyglass"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-ageofgrowing.json`
```json
[
  "hayfork"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-ancienttools.json`
```json
[
  "adze",
  "mallet"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-base.json`
```json
[
  "axe",
  "cleaver",
  "hammer",
  "hoe",
  "knife",
  "pickaxe",
  "prospectingpick",
  "saw",
  "scythe",
  "shovel"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-butchery.json`
```json
[
  "bonepickaxe"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-chiselpick.json`
```json
[
  "chiselpick"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-huntingknife.json`
```json
[
  "huntingknife"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-indappledgroves.json`
```json
[
  "forestaxe",
  "grubaxe",
  "maul"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-paxel.json`
```json
[
  "paxel"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-stonequarry.json`
```json
[
  "chisel-finishing",
  "chisel-wedge",
  "rubblehammer"
]
```
### `assets/toolsmith/config/toolsmith/regex/tinkerabletools/tinkerabletools-tailorsdelight.json`
```json
[
  "awl"
]
```
### `assets/toolsmith/config/toolsmith/regex/toolheads/toolheads-base.json`
```json
[
  "head",
  "blade"
]
```
### `assets/toolsmith/config/toolsmith/regex/toolheads/toolheads-butchery.json`
```json
[
  "toolhead"
]
```
### `assets/toolsmith/config/toolsmith/regex/toolheads/toolheads-stonequarry.json`
```json
[
  "finishingchiselhead",
  "wedgechiselhead"
]
```
### `assets/toolsmith/config/toolsmith/regex/toolheads/toolheads-tailorsdelight.json`
```json
[
  "thorn"
]
```
### `assets/toolsmith/config/toolsmith/regex/woodinbindingshapes/woodinbindingshapes-vanilla.json`
```json
[
  "shovel"
]
```
### `assets/toolsmith/config/toolsmith/stats/bindings/bindings-vanilla.json`
```json
[
  {
    "id": "none",
    "texturePath": "plain",
    "langTag": "binding-none",
    "baseHPfactor": 0.5,
    "selfHPBonus": 0.0,
    "handleHPBonus": 0.0,
    "recoveryPercent": 1.0,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "reeds",
    "texturePath": "game:block/cloth/reedrope",
    "langTag": "binding-reeds",
    "baseHPfactor": 1.0,
    "selfHPBonus": 0.0,
    "handleHPBonus": 0.0,
    "recoveryPercent": 1.0,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "twine",
    "texturePath": "game:block/cloth/basic/normal",
    "langTag": "binding-twine",
    "baseHPfactor": 1.2,
    "selfHPBonus": 0.1,
    "handleHPBonus": 0.05,
    "recoveryPercent": 0.9,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "hide",
    "texturePath": "game:block/cloth/rawhide",
    "langTag": "binding-hide",
    "baseHPfactor": 1.25,
    "selfHPBonus": 0.15,
    "handleHPBonus": 0.05,
    "recoveryPercent": 0.8,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "rope",
    "texturePath": "game:block/cloth/basic/brown",
    "langTag": "binding-rope",
    "baseHPfactor": 1.25,
    "selfHPBonus": 0.15,
    "handleHPBonus": 0.05,
    "recoveryPercent": 0.7,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "cloth",
    "texturePath": "game:block/cloth/linen/plain",
    "langTag": "binding-cloth",
    "baseHPfactor": 1.3,
    "selfHPBonus": 0.2,
    "handleHPBonus": 0.1,
    "recoveryPercent": 0.75,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "leather",
    "texturePath": "game:block/leather/plain",
    "langTag": "binding-leather",
    "baseHPfactor": 1.5,
    "selfHPBonus": 0.3,
    "handleHPBonus": 0.1,
    "recoveryPercent": 0.6,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "sturdy",
    "texturePath": "game:block/leather/chromium",
    "langTag": "binding-sturdy",
    "baseHPfactor": 1.8,
    "selfHPBonus": 0.3,
    "handleHPBonus": 0.25,
    "recoveryPercent": 0.55,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "glue",
    "texturePath": "plain",
    "langTag": "binding-glue",
    "baseHPfactor": 2.0,
    "selfHPBonus": 0.3,
    "handleHPBonus": 0.3,
    "recoveryPercent": 1.0,
    "isMetal": false,
    "metalType": null
  },
  {
    "id": "coppernails",
    "texturePath": "game:block/metal/plate/copper",
    "langTag": "binding-nails-copper",
    "baseHPfactor": 1.4,
    "selfHPBonus": 0.1,
    "handleHPBonus": 0.1,
    "recoveryPercent": 0.9,
    "isMetal": true,
    "metalType": "copper"
  },
  {
    "id": "tinbronzenails",
    "texturePath": "game:block/metal/plate/tinbronze",
    "langTag": "binding-nails-tinbronze",
    "baseHPfactor": 1.7,
    "selfHPBonus": 0.2,
    "handleHPBonus": 0.2,
    "recoveryPercent": 0.5,
    "isMetal": true,
    "metalType": "tinbronze"
  },
  {
    "id": "bismuthbronzenails",
    "texturePath": "game:block/metal/plate/bismuthbronze",
    "langTag": "binding-nails-bismuthbronze",
    "baseHPfactor": 1.7,
    "selfHPBonus": 0.25,
    "handleHPBonus": 0.2,
    "recoveryPercent": 0.5,
    "isMetal": true,
    "metalType": "bismuthbronze"
  },
  {
    "id": "blackbronzenails",
    "texturePath": "game:block/metal/plate/blackbronze",
    "langTag": "binding-nails-blackbronze",
    "baseHPfactor": 1.7,
    "selfHPBonus": 0.3,
    "handleHPBonus": 0.2,
    "recoveryPercent": 0.5,
    "isMetal": true,
    "metalType": "blackbronze"
  },
  {
    "id": "ironnails",
    "texturePath": "game:block/metal/plate/iron",
    "langTag": "binding-nails-iron",
    "baseHPfactor": 1.8,
    "selfHPBonus": 0.3,
    "handleHPBonus": 0.2,
    "recoveryPercent": 0.45,
    "isMetal": true,
    "metalType": "iron"
  },
  {
    "id": "cupronickelnails",
    "texturePath": "game:block/metal/ingot/cupronickel",
    "langTag": "binding-nails-cupronickel",
    "baseHPfactor": 1.7,
    "selfHPBonus": 0.2,
    "handleHPBonus": 0.2,
    "recoveryPercent": 0.5,
    "isMetal": true,
    "metalType": "cupronickel"
  },
  {
    "id": "meteoricironnails",
    "texturePath": "game:block/metal/plate/meteoriciron",
    "langTag": "binding-nails-meteoriciron",
    "baseHPfactor": 1.9,
    "selfHPBonus": 0.5,
    "handleHPBonus": 0.25,
    "recoveryPercent": 0.45,
    "isMetal": true,
    "metalType": "meteoriciron"
  },
  {
    "id": "steelnails",
    "texturePath": "game:block/metal/plate/steel",
    "langTag": "binding-nails-steel",
    "baseHPfactor": 2.2,
    "selfHPBonus": 0.6,
    "handleHPBonus": 0.4,
    "recoveryPercent": 0.35,
    "isMetal": true,
    "metalType": "steel"
  }
]
```
### `assets/toolsmith/config/toolsmith/stats/grips/grips-vanilla.json`
```json
[
  {
    "id": "plain",
    "texturePath": "plain",
    "langTag": "",
    "speedBonus": 0.0,
    "chanceToDamage": 1.0
  },
  {
    "id": "twine",
    "texturePath": "game:block/cloth/reedrope",
    "langTag": "grip-twine",
    "speedBonus": 0.0,
    "chanceToDamage": 0.95
  },
  {
    "id": "cloth",
    "texturePath": "game:block/cloth/linen/normal1",
    "langTag": "grip-cloth",
    "speedBonus": 0.1,
    "chanceToDamage": 0.9
  },
  {
    "id": "leather",
    "texturePath": "game:block/leather/plain",
    "langTag": "grip-leather",
    "speedBonus": 0.2,
    "chanceToDamage": 0.8
  },
  {
    "id": "sturdy",
    "texturePath": "game:block/leather/chromium",
    "langTag": "grip-sturdy",
    "speedBonus": 0.3,
    "chanceToDamage": 0.65
  }
]
```
### `assets/toolsmith/config/toolsmith/stats/handles/handles-vanilla.json`
```json
[
  {
    "id": "stick",
    "baseHPfactor": 1.0,
    "selfHPBonus": 0.0,
    "bindingHPBonus": 0.0,
    "speedBonus": 0.0
  },
  {
    "id": "bone",
    "baseHPfactor": 1.0,
    "selfHPBonus": 0.05,
    "bindingHPBonus": 0.05,
    "speedBonus": 0.0
  },
  {
    "id": "crude",
    "baseHPfactor": 1.0,
    "selfHPBonus": 0.0,
    "bindingHPBonus": 0.05,
    "speedBonus": 0.0
  },
  {
    "id": "handle",
    "baseHPfactor": 1.2,
    "selfHPBonus": 0.05,
    "bindingHPBonus": 0.2,
    "speedBonus": 0.05
  },
  {
    "id": "professional",
    "baseHPfactor": 1.5,
    "selfHPBonus": 0.1,
    "bindingHPBonus": 0.4,
    "speedBonus": 0.1
  }
]
```
### `assets/toolsmith/config/toolsmith/stats/treatments/treatments-vanilla.json`
```json
[
  {
    "id": "none",
    "langTag": "",
    "handleHPbonus": 0.0
  },
  {
    "id": "fat",
    "langTag": "treatment-fat",
    "handleHPbonus": 0.2
  },
  {
    "id": "wax",
    "langTag": "treatment-wax",
    "handleHPbonus": 0.5
  },
  {
    "id": "oil",
    "langTag": "treatment-oil",
    "handleHPbonus": 0.65
  }
]
```
## Exact supplied configs

### `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/Toolsmith.json`
```json
{
  "AutoUpdateConfigsOnVersionChange": true,
  "AccessibilityDisableNeedToHoldClick": false,
  "PrintAllParsedToolsAndParts": false,
  "DebugMessages": false,
  "RunFullJsonVerifying": false,
  "EnableGridRecipesForToolCrafting": false,
  "HeadDurabilityMult": 5.0,
  "SharpnessMult": 1.5,
  "GrindstoneSharpenPerTick": 1.0,
  "HoningDamageMult": 1.0,
  "SharpWear": 0.15,
  "BluntWear": 0.02,
  "PercentDamageForReforge": 1.0,
  "ShouldHoningDamageHead": true,
  "NoBitLossAlternateReforgeGen": false,
  "UseBitsForSmithing": true,
  "ExtraBitVoxelChance": 0.1,
  "EnableEditsForRegex": false,
  "ToolHeads": "@.*(head|blade|toolhead|finishingchiselhead|wedgechiselhead|thorn).*",
  "TinkerableTools": "@.*:(barkspud|pitsaw|maul|hayfork|adze|mallet|axe|cleaver|hammer|hoe|knife|pickaxe|prospectingpick|saw|scythe|shovel|bonepickaxe|chiselpick|huntingknife|forestaxe|grubaxe|maul|paxel|chisel-finishing|chisel-wedge|rubblehammer|awl).*",
  "SinglePartTools": "@.*:(wedge|creaser|flail|chisel|shears|tongs|wrench|cangemchisel|truechisel|handplaner|handwedge|laddermaker|paintbrush|paintscraper|pantograph|pathmaker|rollingpin|spyglass).*",
  "BluntHeadedTools": "@.*:(creaser|flail|mallet|hammer|tongs|wrench|handwedge|laddermaker|paintbrush|pantograph|pathmaker|rollingpin|spyglass|rubblehammer).*",
  "ToolsWithWoodInBindingShape": "@.*(shovel).*",
  "PartBlacklist": "@.*(wfradmin|__armoryfork_toolsmith_allow__|helve|chiseledblock|stickslayer|scrap|ruined|chiseled|wooden|block|chiselmold|wrenchmold|knifemold|sawmill|sawbuck|sawhorse|sawdust|awl-bone|awl-horn|awl-flint|awl-obsidian).*",
  "ModVersionNumber": "1.2.19"
}
```
### `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/ToolsmithClient.json`
```json
{
  "ShouldRenderWorkbenchSlotMarkers": true,
  "ShowAllSharpnessBarSections": false,
  "UseGradientForSharpnessInstead": false,
  "DisableMultiPartRendering": false
}
```
### `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/ToolsmithPartsStats.json`
```json
{
  "EnableEdits": false,
  "BaseHandleParts": {
    "stick": {
      "handleStatTag": "stick",
      "canHaveGrip": false,
      "handleShapePath": "toolsmith:shapes/item/parts/handles/stick/handle",
      "canBeTreated": false,
      "dryingTimeMult": 1.0,
      "enabled": true,
      "id": "stick"
    },
    "bone": {
      "handleStatTag": "bone",
      "canHaveGrip": true,
      "handleShapePath": "toolsmith:shapes/item/parts/handles/bone/handle",
      "canBeTreated": false,
      "dryingTimeMult": 1.0,
      "enabled": true,
      "id": "bone"
    },
    "crudehandle": {
      "handleStatTag": "crude",
      "canHaveGrip": true,
      "handleShapePath": "toolsmith:shapes/item/parts/handles/crude/handle",
      "canBeTreated": false,
      "dryingTimeMult": 1.0,
      "enabled": true,
      "id": "crudehandle"
    },
    "handle": {
      "handleStatTag": "handle",
      "canHaveGrip": true,
      "handleShapePath": "toolsmith:shapes/item/parts/handles/regular/handle",
      "canBeTreated": true,
      "dryingTimeMult": 1.0,
      "enabled": true,
      "id": "handle"
    },
    "carpentedhandle": {
      "handleStatTag": "professional",
      "canHaveGrip": true,
      "handleShapePath": "toolsmith:shapes/item/parts/handles/fine/handle",
      "canBeTreated": true,
      "dryingTimeMult": 2.0,
      "enabled": true,
      "id": "carpentedhandle"
    }
  },
  "GripParts": {
    "leather-normal-acacia": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/acacia",
      "enabled": true,
      "id": "leather-normal-acacia"
    },
    "leather-normal-aged": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/aged",
      "enabled": true,
      "id": "leather-normal-aged"
    },
    "leather-normal-baldcypress": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/baldcypress",
      "enabled": true,
      "id": "leather-normal-baldcypress"
    },
    "leather-normal-birch": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/birch",
      "enabled": true,
      "id": "leather-normal-birch"
    },
    "leather-normal-cypress": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/cypress",
      "enabled": true,
      "id": "leather-normal-cypress"
    },
    "leather-normal-ebony": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/ebony",
      "enabled": true,
      "id": "leather-normal-ebony"
    },
    "leather-normal-kapok": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/kapok",
      "enabled": true,
      "id": "leather-normal-kapok"
    },
    "leather-normal-larch": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/larch",
      "enabled": true,
      "id": "leather-normal-larch"
    },
    "leather-normal-maple": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/maple",
      "enabled": true,
      "id": "leather-normal-maple"
    },
    "leather-normal-pine": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/pine",
      "enabled": true,
      "id": "leather-normal-pine"
    },
    "leather-normal-purpleheart": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/purpleheart",
      "enabled": true,
      "id": "leather-normal-purpleheart"
    },
    "leather-normal-redwood": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/redwood",
      "enabled": true,
      "id": "leather-normal-redwood"
    },
    "leather-normal-walnut": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/leather/walnut",
      "enabled": true,
      "id": "leather-normal-walnut"
    },
    "buckskin": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "ancienttools:item/resource/buckskin",
      "enabled": true,
      "id": "buckskin"
    },
    "cloth-darkblue": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkblue",
      "enabled": true,
      "id": "cloth-darkblue"
    },
    "cloth-darkbrown": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkbrown",
      "enabled": true,
      "id": "cloth-darkbrown"
    },
    "cloth-darkgreen": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkgreen",
      "enabled": true,
      "id": "cloth-darkgreen"
    },
    "cloth-darkred": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "tailorsdelight:block/cloth/dark/darkred",
      "enabled": true,
      "id": "cloth-darkred"
    },
    "flaxtwine": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "",
      "enabled": true,
      "id": "flaxtwine"
    },
    "linen-diamond-down": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/diamond1",
      "enabled": true,
      "id": "linen-diamond-down"
    },
    "linen-normal-down": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/normal1",
      "enabled": true,
      "id": "linen-normal-down"
    },
    "linen-offset-down": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/offset1",
      "enabled": true,
      "id": "linen-offset-down"
    },
    "linen-square-down": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/square1",
      "enabled": true,
      "id": "linen-square-down"
    },
    "cloth-plain": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/normal1",
      "enabled": true,
      "id": "cloth-plain"
    },
    "cloth-mordant": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/mordant",
      "enabled": true,
      "id": "cloth-mordant"
    },
    "cloth-blue": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/blue",
      "enabled": true,
      "id": "cloth-blue"
    },
    "cloth-red": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/red",
      "enabled": true,
      "id": "cloth-red"
    },
    "cloth-yellow": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/yellow",
      "enabled": true,
      "id": "cloth-yellow"
    },
    "cloth-green": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/green",
      "enabled": true,
      "id": "cloth-green"
    },
    "cloth-purple": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/purple",
      "enabled": true,
      "id": "cloth-purple"
    },
    "cloth-pink": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/pink",
      "enabled": true,
      "id": "cloth-pink"
    },
    "cloth-orange": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/orange",
      "enabled": true,
      "id": "cloth-orange"
    },
    "cloth-brown": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/brown",
      "enabled": true,
      "id": "cloth-brown"
    },
    "cloth-gray": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/gray",
      "enabled": true,
      "id": "cloth-gray"
    },
    "cloth-black": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/black",
      "enabled": true,
      "id": "cloth-black"
    },
    "cloth-white": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/white",
      "enabled": true,
      "id": "cloth-white"
    },
    "leather-normal-plain": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "",
      "enabled": true,
      "id": "leather-normal-plain"
    },
    "leather-normal-orange": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/orange",
      "enabled": true,
      "id": "leather-normal-orange"
    },
    "leather-normal-black": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/black",
      "enabled": true,
      "id": "leather-normal-black"
    },
    "leather-normal-red": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/red",
      "enabled": true,
      "id": "leather-normal-red"
    },
    "leather-normal-blue": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/blue",
      "enabled": true,
      "id": "leather-normal-blue"
    },
    "leather-normal-purple": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/purple",
      "enabled": true,
      "id": "leather-normal-purple"
    },
    "leather-normal-pink": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/pink",
      "enabled": true,
      "id": "leather-normal-pink"
    },
    "leather-normal-white": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/white",
      "enabled": true,
      "id": "leather-normal-white"
    },
    "leather-normal-yellow": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/yellow",
      "enabled": true,
      "id": "leather-normal-yellow"
    },
    "leather-normal-gray": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/gray",
      "enabled": true,
      "id": "leather-normal-gray"
    },
    "leather-normal-green": {
      "gripStatTag": "leather",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/leather/green",
      "enabled": true,
      "id": "leather-normal-green"
    },
    "leather-sturdy-plain": {
      "gripStatTag": "sturdy",
      "gripShapePath": "fabric",
      "gripTextureOverride": "",
      "enabled": true,
      "id": "leather-sturdy-plain"
    },
    "twine-wool-mordant": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/mordant",
      "enabled": true,
      "id": "twine-wool-mordant"
    },
    "twine-wool-plain": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/normal1",
      "enabled": true,
      "id": "twine-wool-plain"
    },
    "twine-wool-black": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/black",
      "enabled": true,
      "id": "twine-wool-black"
    },
    "twine-wool-blue": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/blue",
      "enabled": true,
      "id": "twine-wool-blue"
    },
    "twine-wool-brown": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/brown",
      "enabled": true,
      "id": "twine-wool-brown"
    },
    "twine-wool-gray": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/gray",
      "enabled": true,
      "id": "twine-wool-gray"
    },
    "twine-wool-green": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/green",
      "enabled": true,
      "id": "twine-wool-green"
    },
    "twine-wool-orange": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/orange",
      "enabled": true,
      "id": "twine-wool-orange"
    },
    "twine-wool-pink": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/pink",
      "enabled": true,
      "id": "twine-wool-pink"
    },
    "twine-wool-purple": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/purple",
      "enabled": true,
      "id": "twine-wool-purple"
    },
    "twine-wool-red": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/red",
      "enabled": true,
      "id": "twine-wool-red"
    },
    "twine-wool-white": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/white",
      "enabled": true,
      "id": "twine-wool-white"
    },
    "twine-wool-yellow": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/yellow",
      "enabled": true,
      "id": "twine-wool-yellow"
    },
    "twine-wool-darkblue": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkblue",
      "enabled": true,
      "id": "twine-wool-darkblue"
    },
    "twine-wool-darkbrown": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkbrown",
      "enabled": true,
      "id": "twine-wool-darkbrown"
    },
    "twine-wool-darkgreen": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkgreen",
      "enabled": true,
      "id": "twine-wool-darkgreen"
    },
    "twine-wool-darkred": {
      "gripStatTag": "twine",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkred",
      "enabled": true,
      "id": "twine-wool-darkred"
    },
    "cloth-wool-mordant": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "game:block/cloth/linen/mordant",
      "enabled": true,
      "id": "cloth-wool-mordant"
    },
    "cloth-wool-plain": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/plain",
      "enabled": true,
      "id": "cloth-wool-plain"
    },
    "cloth-wool-black": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/black",
      "enabled": true,
      "id": "cloth-wool-black"
    },
    "cloth-wool-blue": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/blue",
      "enabled": true,
      "id": "cloth-wool-blue"
    },
    "cloth-wool-brown": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/brown",
      "enabled": true,
      "id": "cloth-wool-brown"
    },
    "cloth-wool-gray": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/gray",
      "enabled": true,
      "id": "cloth-wool-gray"
    },
    "cloth-wool-green": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/green",
      "enabled": true,
      "id": "cloth-wool-green"
    },
    "cloth-wool-orange": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/orange",
      "enabled": true,
      "id": "cloth-wool-orange"
    },
    "cloth-wool-pink": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/pink",
      "enabled": true,
      "id": "cloth-wool-pink"
    },
    "cloth-wool-purple": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/purple",
      "enabled": true,
      "id": "cloth-wool-purple"
    },
    "cloth-wool-red": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/red",
      "enabled": true,
      "id": "cloth-wool-red"
    },
    "cloth-wool-white": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/white",
      "enabled": true,
      "id": "cloth-wool-white"
    },
    "cloth-wool-yellow": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/yellow",
      "enabled": true,
      "id": "cloth-wool-yellow"
    },
    "cloth-wool-darkblue": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkblue",
      "enabled": true,
      "id": "cloth-wool-darkblue"
    },
    "cloth-wool-darkbrown": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkbrown",
      "enabled": true,
      "id": "cloth-wool-darkbrown"
    },
    "cloth-wool-darkgreen": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkgreen",
      "enabled": true,
      "id": "cloth-wool-darkgreen"
    },
    "cloth-wool-darkred": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkred",
      "enabled": true,
      "id": "cloth-wool-darkred"
    },
    "wool-plain": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/plain",
      "enabled": true,
      "id": "wool-plain"
    },
    "wool-black": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/black",
      "enabled": true,
      "id": "wool-black"
    },
    "wool-blue": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/blue",
      "enabled": true,
      "id": "wool-blue"
    },
    "wool-brown": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/brown",
      "enabled": true,
      "id": "wool-brown"
    },
    "wool-gray": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/gray",
      "enabled": true,
      "id": "wool-gray"
    },
    "wool-green": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/green",
      "enabled": true,
      "id": "wool-green"
    },
    "wool-orange": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/orange",
      "enabled": true,
      "id": "wool-orange"
    },
    "wool-pink": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/pink",
      "enabled": true,
      "id": "wool-pink"
    },
    "wool-purple": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/purple",
      "enabled": true,
      "id": "wool-purple"
    },
    "wool-red": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/red",
      "enabled": true,
      "id": "wool-red"
    },
    "wool-white": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/white",
      "enabled": true,
      "id": "wool-white"
    },
    "wool-yellow": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/linen/yellow",
      "enabled": true,
      "id": "wool-yellow"
    },
    "wool-darkblue": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkblue",
      "enabled": true,
      "id": "wool-darkblue"
    },
    "wool-darkbrown": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkbrown",
      "enabled": true,
      "id": "wool-darkbrown"
    },
    "wool-darkgreen": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkgreen",
      "enabled": true,
      "id": "wool-darkgreen"
    },
    "wool-darkred": {
      "gripStatTag": "cloth",
      "gripShapePath": "fabric",
      "gripTextureOverride": "wool:block/cloth/wool/darkred",
      "enabled": true,
      "id": "wool-darkred"
    }
  },
  "TreatmentParts": {
    "foodoilportion-flax": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.1,
      "enabled": true,
      "id": "foodoilportion-flax"
    },
    "foodoilportion-seed": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.1,
      "enabled": true,
      "id": "foodoilportion-seed"
    },
    "plantoil-flax": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.1,
      "enabled": true,
      "id": "plantoil-flax"
    },
    "plantoil-walnut": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.1,
      "enabled": true,
      "id": "plantoil-walnut"
    },
    "woodfinish-flax": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.05,
      "enabled": true,
      "id": "woodfinish-flax"
    },
    "woodfinish-walnut": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.05,
      "enabled": true,
      "id": "woodfinish-walnut"
    },
    "fat": {
      "treatmentStatTag": "fat",
      "dryingHours": 12,
      "isLiquid": false,
      "litersUsed": 0.0,
      "enabled": true,
      "id": "fat"
    },
    "fat-rendered": {
      "treatmentStatTag": "fat",
      "dryingHours": 12,
      "isLiquid": false,
      "litersUsed": 0.0,
      "enabled": true,
      "id": "fat-rendered"
    },
    "beeswax": {
      "treatmentStatTag": "wax",
      "dryingHours": 12,
      "isLiquid": false,
      "litersUsed": 0.0,
      "enabled": true,
      "id": "beeswax"
    },
    "oilportion-flax": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.1,
      "enabled": true,
      "id": "oilportion-flax"
    },
    "oilportion-olive": {
      "treatmentStatTag": "oil",
      "dryingHours": 24,
      "isLiquid": true,
      "litersUsed": 0.1,
      "enabled": true,
      "id": "oilportion-olive"
    }
  },
  "BindingParts": {
    "fabricstrips-atleather": {
      "bindingStatTag": "leather",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "fabricstrips-atleather"
    },
    "sinew-dry": {
      "bindingStatTag": "rope",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "butchering:item/resource/sinew",
      "enabled": true,
      "id": "sinew-dry"
    },
    "glueportion-sinew-cold": {
      "bindingStatTag": "glue",
      "isLiquid": true,
      "litersUsed": 0.5,
      "bindingShapePath": "",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "glueportion-sinew-cold"
    },
    "glueportion-hide-hot": {
      "bindingStatTag": "glue",
      "isLiquid": true,
      "litersUsed": 0.5,
      "bindingShapePath": "",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "glueportion-hide-hot"
    },
    "cordage": {
      "bindingStatTag": "rope",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "cordage"
    },
    "cord-bamboo": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:bamboo/cord",
      "enabled": true,
      "id": "cord-bamboo"
    },
    "cord-bark": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:bark/cord",
      "enabled": true,
      "id": "cord-bark"
    },
    "cord-coconut": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:coconut/cord",
      "enabled": true,
      "id": "cord-coconut"
    },
    "cord-flax": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:flax/cord",
      "enabled": true,
      "id": "cord-flax"
    },
    "cord-mixed": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:mixed/cord",
      "enabled": true,
      "id": "cord-mixed"
    },
    "cord-nzflax": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:nzflax/cord",
      "enabled": true,
      "id": "cord-nzflax"
    },
    "cord-palm": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:palm/cord",
      "enabled": true,
      "id": "cord-palm"
    },
    "cord-papyrus": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:papyrus/cord",
      "enabled": true,
      "id": "cord-papyrus"
    },
    "cord-porcupine": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:porcupine/cord",
      "enabled": true,
      "id": "cord-porcupine"
    },
    "cord-reed": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:reed/cord",
      "enabled": true,
      "id": "cord-reed"
    },
    "cord-sinew": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:sinew/cord",
      "enabled": true,
      "id": "cord-sinew"
    },
    "cord-straw": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:straw/cord",
      "enabled": true,
      "id": "cord-straw"
    },
    "cord-vine": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:vine/cord",
      "enabled": true,
      "id": "cord-vine"
    },
    "cord-larch": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:larch/cord",
      "enabled": true,
      "id": "cord-larch"
    },
    "cord-tuja": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:tuja/cord",
      "enabled": true,
      "id": "cord-tuja"
    },
    "cord-honeylocust": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:honeylocust/cord",
      "enabled": true,
      "id": "cord-honeylocust"
    },
    "cord-birch": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:birch/cord",
      "enabled": true,
      "id": "cord-birch"
    },
    "cord-brideinwhite": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:brideinwhite/cord",
      "enabled": true,
      "id": "cord-brideinwhite"
    },
    "cord-alder": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:alder/cord",
      "enabled": true,
      "id": "cord-alder"
    },
    "cord-bearnut": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:bearnut/cord",
      "enabled": true,
      "id": "cord-bearnut"
    },
    "cord-willow": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:willow/cord",
      "enabled": true,
      "id": "cord-willow"
    },
    "cord-redwillow": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:redwillow/cord",
      "enabled": true,
      "id": "cord-redwillow"
    },
    "cord-poplar": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:poplar/cord",
      "enabled": true,
      "id": "cord-poplar"
    },
    "cord-aspen": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:aspen/cord",
      "enabled": true,
      "id": "cord-aspen"
    },
    "cord-eucalyptus": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:eucalyptus/cord",
      "enabled": true,
      "id": "cord-eucalyptus"
    },
    "cord-kapok": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:kapok/cord",
      "enabled": true,
      "id": "cord-kapok"
    },
    "cord-sapele": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "stringsense:sapele/cord",
      "enabled": true,
      "id": "cord-sapele"
    },
    "twine-mordant": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/mordant",
      "enabled": true,
      "id": "twine-mordant"
    },
    "twine-black": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/black",
      "enabled": true,
      "id": "twine-black"
    },
    "twine-blue": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/blue",
      "enabled": true,
      "id": "twine-blue"
    },
    "twine-brown": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/brown",
      "enabled": true,
      "id": "twine-brown"
    },
    "twine-gray": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/gray",
      "enabled": true,
      "id": "twine-gray"
    },
    "twine-green": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/green",
      "enabled": true,
      "id": "twine-green"
    },
    "twine-orange": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/orange",
      "enabled": true,
      "id": "twine-orange"
    },
    "twine-pink": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/pink",
      "enabled": true,
      "id": "twine-pink"
    },
    "twine-purple": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/purple",
      "enabled": true,
      "id": "twine-purple"
    },
    "twine-red": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/red",
      "enabled": true,
      "id": "twine-red"
    },
    "twine-white": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/white",
      "enabled": true,
      "id": "twine-white"
    },
    "twine-yellow": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/yellow",
      "enabled": true,
      "id": "twine-yellow"
    },
    "twine-darkblue": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkblue",
      "enabled": true,
      "id": "twine-darkblue"
    },
    "twine-darkbrown": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkbrown",
      "enabled": true,
      "id": "twine-darkbrown"
    },
    "twine-darkgreen": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkgreen",
      "enabled": true,
      "id": "twine-darkgreen"
    },
    "twine-darkred": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "tailorsdelight:block/cloth/dark/darkred",
      "enabled": true,
      "id": "twine-darkred"
    },
    "leatherstrips-plain": {
      "bindingStatTag": "leather",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "leatherstrips-plain"
    },
    "drygrass": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "drygrass"
    },
    "cattailtops": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "cattailtops"
    },
    "papyrustops": {
      "bindingStatTag": "reeds",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "papyrustops"
    },
    "flaxtwine": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "flaxtwine"
    },
    "fabricstrips-rawhide": {
      "bindingStatTag": "hide",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "fabricstrips-rawhide"
    },
    "rope": {
      "bindingStatTag": "rope",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "rope"
    },
    "fabricstrips-cloth": {
      "bindingStatTag": "cloth",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "fabricstrips-cloth"
    },
    "fabricstrips-leather": {
      "bindingStatTag": "leather",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "fabricstrips-leather"
    },
    "fabricstrips-sturdy": {
      "bindingStatTag": "sturdy",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "fabricstrips-sturdy"
    },
    "glueportion-pitch-hot": {
      "bindingStatTag": "glue",
      "isLiquid": true,
      "litersUsed": 0.5,
      "bindingShapePath": "",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "glueportion-pitch-hot"
    },
    "metalnailsandstrips-copper": {
      "bindingStatTag": "coppernails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-copper"
    },
    "metalnailsandstrips-tinbronze": {
      "bindingStatTag": "tinbronzenails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-tinbronze"
    },
    "metalnailsandstrips-bismuthbronze": {
      "bindingStatTag": "bismuthbronzenails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-bismuthbronze"
    },
    "metalnailsandstrips-blackbronze": {
      "bindingStatTag": "blackbronzenails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-blackbronze"
    },
    "metalnailsandstrips-iron": {
      "bindingStatTag": "ironnails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-iron"
    },
    "metalnailsandstrips-cupronickel": {
      "bindingStatTag": "cupronickelnails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-cupronickel"
    },
    "metalnailsandstrips-meteoriciron": {
      "bindingStatTag": "meteoricironnails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-meteoriciron"
    },
    "metalnailsandstrips-steel": {
      "bindingStatTag": "steelnails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metalnailsandstrips-steel"
    },
    "metal-parts": {
      "bindingStatTag": "cupronickelnails",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "metal",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "metal-parts"
    },
    "twine-wool-mordant": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/mordant",
      "enabled": true,
      "id": "twine-wool-mordant"
    },
    "twine-wool-plain": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/plain",
      "enabled": true,
      "id": "twine-wool-plain"
    },
    "twine-wool-black": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/black",
      "enabled": true,
      "id": "twine-wool-black"
    },
    "twine-wool-blue": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/blue",
      "enabled": true,
      "id": "twine-wool-blue"
    },
    "twine-wool-brown": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/brown",
      "enabled": true,
      "id": "twine-wool-brown"
    },
    "twine-wool-gray": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/gray",
      "enabled": true,
      "id": "twine-wool-gray"
    },
    "twine-wool-green": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/green",
      "enabled": true,
      "id": "twine-wool-green"
    },
    "twine-wool-orange": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/orange",
      "enabled": true,
      "id": "twine-wool-orange"
    },
    "twine-wool-pink": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/pink",
      "enabled": true,
      "id": "twine-wool-pink"
    },
    "twine-wool-purple": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/purple",
      "enabled": true,
      "id": "twine-wool-purple"
    },
    "twine-wool-red": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/red",
      "enabled": true,
      "id": "twine-wool-red"
    },
    "twine-wool-white": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/white",
      "enabled": true,
      "id": "twine-wool-white"
    },
    "twine-wool-yellow": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "game:block/cloth/linen/yellow",
      "enabled": true,
      "id": "twine-wool-yellow"
    },
    "twine-wool-darkblue": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "wool:block/cloth/wool/darkblue",
      "enabled": true,
      "id": "twine-wool-darkblue"
    },
    "twine-wool-darkbrown": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "wool:block/cloth/wool/darkbrown",
      "enabled": true,
      "id": "twine-wool-darkbrown"
    },
    "twine-wool-darkgreen": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "wool:block/cloth/wool/darkgreen",
      "enabled": true,
      "id": "twine-wool-darkgreen"
    },
    "twine-wool-darkred": {
      "bindingStatTag": "twine",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "wool:block/cloth/wool/darkred",
      "enabled": true,
      "id": "twine-wool-darkred"
    },
    "fabricstrips-wool": {
      "bindingStatTag": "cloth",
      "isLiquid": false,
      "litersUsed": 0.0,
      "bindingShapePath": "string",
      "bindingTextureOverride": "",
      "enabled": true,
      "id": "fabricstrips-wool"
    }
  },
  "BaseHandleStats": {
    "stick": {
      "baseHPfactor": 1.0,
      "selfHPBonus": 0.0,
      "bindingHPBonus": 0.0,
      "speedBonus": 0.0,
      "id": "stick"
    },
    "bone": {
      "baseHPfactor": 1.0,
      "selfHPBonus": 0.05,
      "bindingHPBonus": 0.05,
      "speedBonus": 0.0,
      "id": "bone"
    },
    "crude": {
      "baseHPfactor": 1.0,
      "selfHPBonus": 0.0,
      "bindingHPBonus": 0.05,
      "speedBonus": 0.0,
      "id": "crude"
    },
    "handle": {
      "baseHPfactor": 1.2,
      "selfHPBonus": 0.05,
      "bindingHPBonus": 0.2,
      "speedBonus": 0.05,
      "id": "handle"
    },
    "professional": {
      "baseHPfactor": 1.5,
      "selfHPBonus": 0.1,
      "bindingHPBonus": 0.4,
      "speedBonus": 0.1,
      "id": "professional"
    }
  },
  "GripStats": {
    "plain": {
      "texturePath": "plain",
      "langTag": "",
      "speedBonus": 0.0,
      "chanceToDamage": 1.0,
      "id": "plain"
    },
    "twine": {
      "texturePath": "game:block/cloth/reedrope",
      "langTag": "grip-twine",
      "speedBonus": 0.0,
      "chanceToDamage": 0.95,
      "id": "twine"
    },
    "cloth": {
      "texturePath": "game:block/cloth/linen/normal1",
      "langTag": "grip-cloth",
      "speedBonus": 0.1,
      "chanceToDamage": 0.9,
      "id": "cloth"
    },
    "leather": {
      "texturePath": "game:block/leather/plain",
      "langTag": "grip-leather",
      "speedBonus": 0.2,
      "chanceToDamage": 0.8,
      "id": "leather"
    },
    "sturdy": {
      "texturePath": "game:block/leather/chromium",
      "langTag": "grip-sturdy",
      "speedBonus": 0.3,
      "chanceToDamage": 0.65,
      "id": "sturdy"
    }
  },
  "TreatmentStats": {
    "none": {
      "langTag": "",
      "handleHPbonus": 0.0,
      "id": "none"
    },
    "fat": {
      "langTag": "treatment-fat",
      "handleHPbonus": 0.2,
      "id": "fat"
    },
    "wax": {
      "langTag": "treatment-wax",
      "handleHPbonus": 0.5,
      "id": "wax"
    },
    "oil": {
      "langTag": "treatment-oil",
      "handleHPbonus": 0.65,
      "id": "oil"
    }
  },
  "BindingStats": {
    "none": {
      "texturePath": "plain",
      "langTag": "binding-none",
      "baseHPfactor": 0.5,
      "selfHPBonus": 0.0,
      "handleHPBonus": 0.0,
      "recoveryPercent": 1.0,
      "isMetal": false,
      "metalType": null,
      "id": "none"
    },
    "reeds": {
      "texturePath": "game:block/cloth/reedrope",
      "langTag": "binding-reeds",
      "baseHPfactor": 1.0,
      "selfHPBonus": 0.0,
      "handleHPBonus": 0.0,
      "recoveryPercent": 1.0,
      "isMetal": false,
      "metalType": null,
      "id": "reeds"
    },
    "twine": {
      "texturePath": "game:block/cloth/basic/normal",
      "langTag": "binding-twine",
      "baseHPfactor": 1.2,
      "selfHPBonus": 0.1,
      "handleHPBonus": 0.05,
      "recoveryPercent": 0.9,
      "isMetal": false,
      "metalType": null,
      "id": "twine"
    },
    "hide": {
      "texturePath": "game:block/cloth/rawhide",
      "langTag": "binding-hide",
      "baseHPfactor": 1.25,
      "selfHPBonus": 0.15,
      "handleHPBonus": 0.05,
      "recoveryPercent": 0.8,
      "isMetal": false,
      "metalType": null,
      "id": "hide"
    },
    "rope": {
      "texturePath": "game:block/cloth/basic/brown",
      "langTag": "binding-rope",
      "baseHPfactor": 1.25,
      "selfHPBonus": 0.15,
      "handleHPBonus": 0.05,
      "recoveryPercent": 0.7,
      "isMetal": false,
      "metalType": null,
      "id": "rope"
    },
    "cloth": {
      "texturePath": "game:block/cloth/linen/plain",
      "langTag": "binding-cloth",
      "baseHPfactor": 1.3,
      "selfHPBonus": 0.2,
      "handleHPBonus": 0.1,
      "recoveryPercent": 0.75,
      "isMetal": false,
      "metalType": null,
      "id": "cloth"
    },
    "leather": {
      "texturePath": "game:block/leather/plain",
      "langTag": "binding-leather",
      "baseHPfactor": 1.5,
      "selfHPBonus": 0.3,
      "handleHPBonus": 0.1,
      "recoveryPercent": 0.6,
      "isMetal": false,
      "metalType": null,
      "id": "leather"
    },
    "sturdy": {
      "texturePath": "game:block/leather/chromium",
      "langTag": "binding-sturdy",
      "baseHPfactor": 1.8,
      "selfHPBonus": 0.3,
      "handleHPBonus": 0.25,
      "recoveryPercent": 0.55,
      "isMetal": false,
      "metalType": null,
      "id": "sturdy"
    },
    "glue": {
      "texturePath": "plain",
      "langTag": "binding-glue",
      "baseHPfactor": 2.0,
      "selfHPBonus": 0.3,
      "handleHPBonus": 0.3,
      "recoveryPercent": 1.0,
      "isMetal": false,
      "metalType": null,
      "id": "glue"
    },
    "coppernails": {
      "texturePath": "game:block/metal/plate/copper",
      "langTag": "binding-nails-copper",
      "baseHPfactor": 1.4,
      "selfHPBonus": 0.1,
      "handleHPBonus": 0.1,
      "recoveryPercent": 0.9,
      "isMetal": true,
      "metalType": "copper",
      "id": "coppernails"
    },
    "tinbronzenails": {
      "texturePath": "game:block/metal/plate/tinbronze",
      "langTag": "binding-nails-tinbronze",
      "baseHPfactor": 1.7,
      "selfHPBonus": 0.2,
      "handleHPBonus": 0.2,
      "recoveryPercent": 0.5,
      "isMetal": true,
      "metalType": "tinbronze",
      "id": "tinbronzenails"
    },
    "bismuthbronzenails": {
      "texturePath": "game:block/metal/plate/bismuthbronze",
      "langTag": "binding-nails-bismuthbronze",
      "baseHPfactor": 1.7,
      "selfHPBonus": 0.25,
      "handleHPBonus": 0.2,
      "recoveryPercent": 0.5,
      "isMetal": true,
      "metalType": "bismuthbronze",
      "id": "bismuthbronzenails"
    },
    "blackbronzenails": {
      "texturePath": "game:block/metal/plate/blackbronze",
      "langTag": "binding-nails-blackbronze",
      "baseHPfactor": 1.7,
      "selfHPBonus": 0.3,
      "handleHPBonus": 0.2,
      "recoveryPercent": 0.5,
      "isMetal": true,
      "metalType": "blackbronze",
      "id": "blackbronzenails"
    },
    "ironnails": {
      "texturePath": "game:block/metal/plate/iron",
      "langTag": "binding-nails-iron",
      "baseHPfactor": 1.8,
      "selfHPBonus": 0.3,
      "handleHPBonus": 0.2,
      "recoveryPercent": 0.45,
      "isMetal": true,
      "metalType": "iron",
      "id": "ironnails"
    },
    "cupronickelnails": {
      "texturePath": "game:block/metal/ingot/cupronickel",
      "langTag": "binding-nails-cupronickel",
      "baseHPfactor": 1.7,
      "selfHPBonus": 0.2,
      "handleHPBonus": 0.2,
      "recoveryPercent": 0.5,
      "isMetal": true,
      "metalType": "cupronickel",
      "id": "cupronickelnails"
    },
    "meteoricironnails": {
      "texturePath": "game:block/metal/plate/meteoriciron",
      "langTag": "binding-nails-meteoriciron",
      "baseHPfactor": 1.9,
      "selfHPBonus": 0.5,
      "handleHPBonus": 0.25,
      "recoveryPercent": 0.45,
      "isMetal": true,
      "metalType": "meteoriciron",
      "id": "meteoricironnails"
    },
    "steelnails": {
      "texturePath": "game:block/metal/plate/steel",
      "langTag": "binding-nails-steel",
      "baseHPfactor": 2.2,
      "selfHPBonus": 0.6,
      "handleHPBonus": 0.4,
      "recoveryPercent": 0.35,
      "isMetal": true,
      "metalType": "steel",
      "id": "steelnails"
    }
  }
}
```
## Bundled English handbook evidence
Source: `assets/toolsmith/lang/en.json`; page definitions: `assets/toolsmith/config/handbook/*.json`.

### `game:craftinginfo-toolsmithingbasics-title`
Toolsmithing - The Basics

### `game:craftinginfo-toolsmithingbasics-text`
<strong>Toolsmithing</strong><br><i>Let me show you how to take care of that hatchet.</i><br>The first things you will notice are that some of your tools have multiple parts, as well as a second sharpness bar above the durability. Any tool that has three or more parts is a 'Tinkered Tool', while others that might be crafted in the grid or smithed and come out as one full tool are 'Smithed Tools'. That is the important distinction, Tinkered Tools can be customized and the parts chosen and crafted to increase that tool's durability and more, while a Smithed Tool is more singular, but both are capable of being maintained and repaired. A lot of information can also be found in various tooltips, so read those as well if you are confused!<br><br><strong>Crafting Methods</strong><br>As mentioned before, there are a lot of ways to customize your tools now, along with how you choose to craft them now as well. The old reliable option of the grid crafting is still viable, with expanded options for your handles and (optional) bindings. Just place a handle under the tool head and any binding to the right of it to put it together.<br>A second option is In-Hand crafting for Tinkered Tools! To start this, grab your tool head of choice and hold it in your main hand and stick your chosen handle in your offhand. Hold rightclick for a while to bundle them together as a single part - then follow suit with this pair in your main hand and an optional binding in the offhand to finish it! The binding is not required though, you can leave the offhand empty if you so choose. Happy tinkering!<br><br><strong>Sharpness and Wear</strong><br>Finally, that second bar - the Sharpness bar - is now found on any tool that does not have a blunt edge and needs to be sharp to be able to function. It's important to not let it run out, or your tools will take more damage - and a Drawback (not implemented yet!) is guaranteed to show up. To manage your tool's sharpness, it simply requires either a Knapped Whetstone, made out of any of the knappable stones, or a Grindstone.<br>A Grindstone can be crafted once you have access to boards and a proper piece of rock - abrasive stones like Sandstone and Granite work great for this, but also it is possible to get by with shale or andesite as well. Otherwise, a Whetstone is great for on-the-go sharpening; while the Grindstone being bulky, is best left at home in your workshop!

### `game:craftinginfo-toolsmithcraftingmethods-title`
Toolsmithing - Crafting and Deconstruction Methods

### `game:craftinginfo-toolsmithcraftingmethods-text`
<strong>In-Hand Crafting</strong><br>The primary method to craft your Tinkered Tools is to use the In-Hand crafting method. Just like the crafting grid, it can be accessed anywhere, since all you need are both hands freed. First, grab the parts you wish to use, for a regular Tool that will be a tool head, handle and optionally a binding. The binding is not required, but it will reinforce your tool's binding durability as well as provide some further benefits to your handle if it is of higher quality.<br>To actually use the in-hand crafting, take your tool head in the main hand, and put your handle of choice in the offhand. Check the tooltips to see what makes a valid handle and binding! Hold rightclick for a short while and you will pair that head with that handle for the first step of the crafting process. Then if you wish to use a binding, stick that in your offhand now and rightclick with the too pair in the main hand once more, and after another short while, you will have constructed your tool!<br><br><strong>Workbench Crafting</strong><br>You are also capable of crafting your tools on your trusty Workbench! Those five slots in the middle? That's where you have ample space to set down your parts for crafting - and once you have them all set and prepared, grab your hammer once again. Give any of those parts a nice whack with rightclick a few times to put that tool together nicely. Also, these same slots can be used to average out the durability of any metal head or smithed tool who's recipe outputs multiples. This is very handy for getting them ready for a Reforge!<br><br><strong>Grid Crafting</strong><br>Finally, another option for putting together a tool is the classic option, just using the crafting grid. If you put your tool head of choice above any handle, and a binding to the right slot of the head, you can optionally use this method to craft your tools instead. It is otherwise the same as In-Hand crafting! These recipes are generated when the game loads, so they will generate more based on the configs on the server.<br><br><strong>Deconstruction</strong><br>You are also able to take a Tinkered Tool apart in the case you want to retrieve either the head or handle early! This requires either a Grindstone or a Workbench around, both of which require boards to craft, but this is generally needed only after you have gotten metal tool heads anyway. One important thing to note about Deconstructing your tool is that if you have used it at all, you will be unable to recover the binding from it since you will have to break it apart to not damage the handle or head any.<br>To deconstruct on a Grindstone, simply look at it with your tool in the main hand and hold shift while you rightclick on it. Hold this for a few seconds to take the tool apart! This is the same for the Workbench, except you simply have to look to the left of the bench at the Vise on it, and interact similarly with that.

### `game:craftinginfo-toolsmithdurabilityandwear-title`
Toolsmithing - Durability, Sharpness and Wear

### `game:craftinginfo-toolsmithdurabilityandwear-text`
<strong>Sharpness and Durability</strong><br>The first thing to note about any sharp tool, is it's sharpness level. As expected of forging a tool head, it's impossible to get it perfectly sharp from the act of forging alone, so the tool head will require some honing even after being made. While your tool remains sharp, it will be easier to use it and convey a small use speed bonus of 5%. Also when it remains above 98% total sharpness, your bindings and tool head will not be damaged, and the handle resists damage until the sharpness falls below 95%. For a smithed tool, the only part that matters is the head damage though!<br>If a tool is dull enough to start taking damage to it's head, it will have a chance on every use to take damage. For a sharp tool, this chance is 15%, but for a blunt tool it is only 2%. These values are configurable! Handles and Bindings on the other hand will take 1 damage every use.<br>Finally, when the sharpness gets below 33%, you will find it takes longer to use your tool. It gains a use speed penalty at this point of -10% - and should it ever drop to 0 and become fully dull, the tool will gain a Drawback guaranteed and now the tool head always will take 2 damage on any use.<br><br><strong>Wear and Restoring Sharpness</strong><br>There are two ways to restore sharpness, a Whetstone or a Grindstone. Whetstones will be the first to get your hands on, they simply only need to be knapped from any knappable stone. A Grindstone requires either an abrasive stone like Granite or Sandstone, or a stone like Andesite or Shale to make the stone out of and boards for a frame. The main difference between the two is that your Whetstone has limited durability, while the Grindstone does not - but it is bulky and hard to carry around. To use them, just hold a Whetstone in your offhand and the tool you wish to sharpen in your main hand then hold rightclick! For a Grindstone, just place it down and take your tool in your hands and hold rightclick once again while looking at it.<br>The important thing to note about honing your tool sharp again is that it consumes metal from the tool's head - durability. When you start honing your tool, for the first 20% of total sharpness that you hone, the head will take double damage. After this, between 20-40%, it starts to scale down to 1. This is almost all the damage your tool will take when you are honing it - after this point it curves down to a 5% chance to even damage at all at 50% total sharpness honed, and stays there until it is fully sharp.

### `game:craftinginfo-toolsmithreforging-title`
Toolsmithing - Secrets of the Reforge

### `game:craftinginfo-toolsmithreforging-text`
<strong>Reforging</strong><br>When your tool heads start to get low on durability, you might want to think about replacing them. But that isn't needed any longer! Now you are able to Reforge your treasured tool heads at the Workbench, crafted out of boards. To initiate a Reforge, look over to the toolbox on the right and the spot infront of it. Stick your tool head or smithed tool down there, and grab your hammer to simply rightclick on that slot to prepare the tool for a reforge! This will convert it to a workpiece that you must bring over to your forge to fix it up again.<br>If your head or smithed tool of choice has a recipe that produces more then one item, you will need to make sure you have the correct number of pieces that the recipe outputs to be able to initiate the Reforge. The Workbench can help with this though! Check the Workbench crafting entry for more details - but you can use it for merging them back and averaging the durabilities out between them all.

### `game:craftinginfo-toolsmithmiscaddons-title`
Toolsmithing - Misc Additions

### `game:craftinginfo-toolsmithmiscaddons-text`
<strong>Cold-Forging Copper</strong><br>As a realistic element, Toolsmith allows you to cold-forge copper! This means that you don't need to heat up your copper to work it, it can be worked at room temp because it is such a soft metal.<br><br>More to come as well!

### `game:craftinginfo-toolsmithtooltypes-title`
Toolsmithing - Tooltypes and You

### `game:craftinginfo-toolsmithtooltypes-text`
<strong>Tinkered Tools</strong><br>A Tinkered Tool is any regular tool that you make out of a tool head and a stick. With Toolsmith, these can be upgraded to use better handles, which can be treated to protect the wood and even given a proper grip, as well as a proper binding material to really keep that tool head on that handle longer. Each part of your tool has a separate durability now as well, with weaker or less advanced options functioning for a shorter amount of time before they might fall apart or snap. Remember! There are a few ways to craft them as well, check that handbook page for a better breakdown - but a Tinkered Tool needs a Tool Head to determine what tool it is, and a Handle. A binding is optional, but you will find your tool's head will go flying off the handle quickly!<br><br><strong>Smithed Tools</strong><br>If a tool is not a Tinkered Tool, it will be a Smithed Tool instead! These are any tools that are created through smithing where once you finish the work item, you recieve the completed functional item - like the Chisel! It also includes any more uniquely functional or crafted tools, mostly from other mods: Like a good number of the Chisel Tools from QP Chisel Tools, or the Creaser or Thresher Flail from Age of Growing. These tools instead have one functional part that acts as the Tool's Head, or in this case, the Tool itself! If they are made of metal, you can still reforge them when they get low on durability.<br><br><strong>Blunt Tools</strong><br>A Blunt Tool can be any tool type - it is more of a descriptor for the type of tool then anything. As you can figure from the name, a blunt tool doesn't care about sharpness at all and doesn't need to be honed to work well! It won't gain any benefits nor drawbacks from sharpness though. But you will find that they are unusually sturdy, with a much lower chance to damage the head on use then a sharp tool - and if it is a blunt Tinkered Tool, you still want to watch out for the Handle and Bindings. Keep an eye on those!

### `game:craftinginfo-toolsmithtoolparts-title`
Toolsmithing - Tool Parts and What To Do

### `game:craftinginfo-toolsmithtoolparts-text`
<strong>Tool Heads</strong><br>The Tool Head is probably the most important part. It is what determines what kind of tool you are crafting, of course! But also it's material will determine the overall durability of the rest of it's component parts - so metals are better then stone and wood, while more advanced metals can still be used for longer. If a tool is not Blunt, it will also have a Sharpness value that you can hone to increase and restore, at the expense of the heads's durability. Check out the Tooltips if you ever forget!<br><br><strong>Tool Handles</strong><br>A Tool Handle can be made from a few options, your usual stick or bone can be a good starting material, but after a while you may wish to craft better options, like a Crude Handle or a Carved Handle! Both Carved and Professional Handles can have their wood treated with a few options, as well as given a grip to keep your hands steady. But a Crude Handle only can reliably have a grip attached, as it was simply cut from a spare piece of firewood. Both of these actions will increase the benefits your handle will give, when you have one be sure to check the Tooltip for more specific information! Your base handle will determine most of it's durability, while more advanced handles can also provide the Binding with more support and additional durability, as well as let you use the tool faster overall.<br><br><strong>Treatment Options</strong><br>Treating your handle is optional, but if you do it, you will find it is able to resist wear and the elements a lot longer - a treatment provides a substantial durability bonus! There are a few options for treatments, like Fat, Beeswax or Oil and in that order they are from the easiest and least effective, to the strongest. Oil can only be made through cross-compatability though, with Flax and Seed Oil from Expanded Foods serving as great tool treatment options, as well as Linseed and Walnut Oil from the Oils mod. In addition, both wood finishes do the job wonderfully at even less consumed!<br><br><strong>Grips</strong><br>A good handle grip will convey two bonuses to your handle, a use speed increase as well as a chance for the handle to not take any damage. Some great materials for a grip include Twine, Linen squares, any color of Leather for some decoration as well, or even Sturdy Leather if you can afford it. Again check the Tooltips of your handle to see more specific values for each!<br><br><strong>Tool Bindings</strong><br>A good sturdy tool is never found without a good binding! You are able to make a Tinkered Tool without one, but be warned that the head will fly off the handle before long and need to be re-attached. To solve this though, you simply only need to use dry grass or any form of reeds to lash that head onto the handle again. Your choice of binding conveys mainly it's level of durability, a small bonus towards the handle's durability, and finally how resilliant the binding is and at what percentage of durability remaining can the binding be salvaged if another part of the tool fails. Some more advanced options for bindings include Twine, Rope, Leather of any color, Hot Pitch Glue along with other glue options from Butchery, nails of most metals, and Metal Parts. Search the Handbook and check item Tooltips for more information!


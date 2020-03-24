<template>
  <div id="view-settings">
    <div id="settings-overlay" v-if="shown" @click="closeSettings"></div>
    <div id="settings" :class="{shown}">
      <div class="setting">
        <div class="setting-title">{{$t('general.language')}}</div>
        <div>
          <el-select v-model="$i18n.locale" @change="selectLanguage" style="width: 100%">
            <el-option
              v-for="(lang, i) in languages"
              :key="`Lang${i}`"
              :label="lang.label"
              :value="lang.value"
            >
              <span style="float: left">{{ lang.label }}</span>
              <span style="float: right; color: #8492a6; font-size: 13px">{{ lang.value }}</span>
            </el-option>
          </el-select>
        </div>
      </div>
      <div class="setting">
        <div class="setting-title">{{$t('general.theme')}}</div>
        <div>
          <el-select v-model="selectedTheme" @change="selectTheme" style="width: 100%">
            <el-option
              v-for="(theme, i) in themes"
              :key="`Theme${i}`"
              :label="theme.label"
              :value="theme.value"
            >{{ theme.label }}</el-option>
          </el-select>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import languageIndex from '@/i18n/index.json';

export default {
  name: 'settings',
  data() {
    return {
      languages: languageIndex.languages,
      selectedTheme: this.$store.state.ui.theme,
    };
  },
  computed: {
    shown() {
      return this.$store.state.ui.settingsShown;
    },
    themes() {
      return [
        {
          value: 'dark',
          label: this.$t('themes.dark'),
        },
        {
          value: 'light',
          label: this.$t('themes.light'),
        },
      ];
    },
  },
  methods: {
    closeSettings: function() {
      this.$store.commit('hideSettings');
    },
    selectLanguage: function(language) {
      localStorage.setItem('language', language);
    },
    selectTheme: function(theme) {
      document.documentElement.classList.remove(...this.themes.map(t => t.value));
      document.documentElement.classList.add(theme);
      this.$store.commit('setTheme', theme);
      localStorage.setItem('theme', theme);
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

#settings-overlay {
  position: fixed;
  top: 56px;
  left: 0px;
  right: 0px;
  bottom: 0px;
  background-color: black;
  opacity: 0.5;
}

#settings {
  position: fixed;
  top: 56px;
  right: 0px;
  bottom: 0px;
  width: 0px;
  overflow: hidden;
  background-color: var($--theme-foreground);
  transition: all 0.25s ease-in-out;
  &.shown {
    width: 300px;
  }
  text-align: left;
}

.setting {
  margin: 16px 16px 0px;
}

.setting > div {
  padding: 8px 0px;
}

.setting-title {
  border-bottom: 1px solid var($--theme-primary);
}
</style>
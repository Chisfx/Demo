<template>
  <v-app id="inspire">
    <v-navigation-drawer
        permanent
        expand-on-hover
      >
        <v-list>       
          <v-list-item link>
            <v-list-item-content>
              <v-list-item-title class="text-h6">
                Demo
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>

        <v-divider></v-divider>

        <v-list
          nav
          dense
        >
          <v-list-item :to="{ name: 'home'}">
            <v-list-item-icon>
              <v-icon>home</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Inicio</v-list-item-title>
          </v-list-item>     
          <v-list-item :to="{ name: 'users'}">
            <v-list-item-icon>
              <v-icon>group</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Usuarios</v-list-item-title>
          </v-list-item>
        </v-list>
    </v-navigation-drawer>    
    <v-main>
      <v-container class="">
        <router-view/>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
  export default {
    //
  }
</script>